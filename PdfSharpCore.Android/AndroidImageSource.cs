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

        protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
        {
            return new AndroidImageSourceImpl(name, () => { return new MemoryStream(imageSource.Invoke()); }, (int)quality);
        }

        protected override IImageSource FromFileImpl(string path, int? quality = 75)
        {
            return new AndroidImageSourceImpl(path, () => { return File.OpenRead(path); }, (int)quality);
        }

        protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
        {
            return new AndroidImageSourceImpl(name, imageStream, (int)quality);
        }

        /*
         * Since Android is quick to run into an out of memory exception (when using larger images), we clean memory as often as possible, though this might lead to the same resize operation multiple times.
         */
        private class AndroidImageSourceImpl : IImageSource
        {
            private Orientation Orientation { get; }
            private Options Options { get; }

            private readonly Func<Stream> _streamSource;
            private readonly int _quality;

            public int Width => Options.OutHeight;
            public int Height => Options.OutHeight;
            public string Name { get; }

            public AndroidImageSourceImpl(string name, Func<Stream> streamSource, int quality)
            {
                Name = name;
                _streamSource = streamSource;
                _quality = quality;
                using (var stream = streamSource.Invoke())
                {
                    Orientation = ImageMetadataReader.ReadMetadata(stream)
                        .OfType<ExifIfd0Directory>()
                        .Where(x => x.HasTagName(ExifDirectoryBase.TagOrientation))
                        .Select(x => (Orientation?)x.GetInt32(ExifDirectoryBase.TagOrientation))
                        .FirstOrDefault() ?? Orientation.Normal;
                    stream.Seek(0, SeekOrigin.Begin);
                    Options = new Options { InJustDecodeBounds = true };
#pragma warning disable CS0642 // Possible mistaken empty statement
                    using (DecodeStream(stream, null, Options)) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                }
            }

            public void SaveAsJpeg(MemoryStream ms)
            {
                Matrix mx = new Matrix();
                using (var bitmap = DecodeStream(ms))
                {
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
                            bitmap.Compress(CompressFormat.Jpeg, _quality, ms);
                            return;
                    }
                    using (var flip = Bitmap.CreateBitmap(bitmap, 0, 0, Options.OutWidth, Options.OutHeight, mx, true))
                    {
                        flip.Compress(CompressFormat.Jpeg, _quality, ms);
                    }
                }
            }
        }
    }
}
