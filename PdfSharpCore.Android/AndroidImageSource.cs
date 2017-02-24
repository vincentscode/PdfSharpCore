using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using static Android.Graphics.BitmapFactory;
using Android.Graphics;
using static Android.Graphics.Bitmap;
using MetadataExtractor;
using System.Linq;
using MetadataExtractor.Formats.Exif;

namespace PdfSharpCore.ImageSharp
{
    public class AndroidImageSource : ImageSource
    {
        private enum Orientation
        {
            Normal = 1,
            Rotate90 = 6,
            Rotate180 = 3,
            Rotate270 = 8
        }

        protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource)
        {
            return new AndroidImageSourceImpl(name, () => { return new MemoryStream(imageSource.Invoke()); });
        }

        protected override IImageSource FromFileImpl(string path)
        {
            return new AndroidImageSourceImpl(path, () => { return File.OpenRead(path); });
        }

        protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream)
        {
            return new AndroidImageSourceImpl(name, imageStream);
        }

        /*
         * Since Android is quick to run into an out of memory exception (when using larger images), we clean memory as often as possible, though this might lead to the same resize operation multiple times.
         */
        private class AndroidImageSourceImpl : IImageSource
        {
            private Bitmap _bitmap;
            private Bitmap Bitmap
            {
                get
                {
                    if (_bitmap == null)
                    {
                        if (_bitmapOptions == null)
                        {
                            _bitmapOptions = new Options { InJustDecodeBounds = false };
                            Orientation = GetOrientation();
                            Stream.Seek(0, SeekOrigin.Begin);
                        }
                        else
                        {
                            _bitmapOptions.InJustDecodeBounds = false;
                        }
                        _bitmap = DecodeStream(Stream, null, _bitmapOptions);
                        //Once we've read the stream, we don't need it anymore
                        _stream.Dispose();
                        _stream = null;
                    }
                    return _bitmap;
                }
            }

            private Orientation Orientation;
            private Orientation GetOrientation()
            {
                return ImageMetadataReader.ReadMetadata(Stream)
                    .OfType<ExifIfd0Directory>()
                    .Where(x => x.HasTagName(ExifDirectoryBase.TagOrientation))
                    .Select(x => (Orientation?)x.GetInt32(ExifDirectoryBase.TagOrientation))
                    .FirstOrDefault() ?? Orientation.Normal;
            }

            private Options _bitmapOptions;
            private Options BitmapOptions
            {
                get
                {
                    if (_bitmapOptions == null)
                    {
                        _bitmapOptions = new Options { InJustDecodeBounds = true };
#pragma warning disable CS0642 // Possible mistaken empty statement
                        using (DecodeStream(Stream, null, _bitmapOptions)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                        Stream.Seek(0, SeekOrigin.Begin);
                        Orientation = GetOrientation();
                        Stream.Seek(0, SeekOrigin.Begin);
                    }
                    return _bitmapOptions;
                }
            }

            private readonly Func<Stream> _streamSource;
            private Stream _stream;
            private Stream Stream
            {
                get
                {
                    if (_stream == null)
                    {
                        _stream = _streamSource.Invoke();
                    }
                    return _stream;
                }
            }

            public int Width => BitmapOptions.OutWidth;
            public int Height => BitmapOptions.OutHeight;
            public string Name { get; }

            public AndroidImageSourceImpl(string name, Func<Stream> streamSource)
            {
                Name = name;
                _streamSource = streamSource;
            }

            public void SaveAsJpeg(MemoryStream ms)
            {
                Matrix mx = new Matrix();

                switch (Orientation)
                {
                    case Orientation.Rotate90:
                        mx.PostRotate(90);
                        break;
                    case Orientation.Rotate180:
                        mx.PostRotate(180);

                        break;
                    case Orientation.Rotate270:
                        mx.PostRotate(270);
                        break;
                    default:
                        Bitmap.Compress(CompressFormat.Jpeg, 50, ms);
                        //once we've created the jpeg, we don't need the bitmap anymore
                        Bitmap.Dispose();
                        _bitmap = null;
                        return;
                }
                using (var flip = Bitmap.CreateBitmap(Bitmap, 0, 0, BitmapOptions.OutWidth, BitmapOptions.OutHeight, mx, true))
                {
                    //once we've created the jpeg, we don't need the bitmap anymore
                    Bitmap.Dispose();
                    _bitmap = null;
                    flip.Compress(CompressFormat.Jpeg, 50, ms);
                }
            }

            public void Dispose()
            {
                _bitmap?.Dispose();
                _bitmap = null;
                _stream?.Dispose();
                _stream = null;
            }
        }
    }
}
