using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ImageSharp;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using static MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharpCore.ImageSharp
{
    public class ImageSharpImageSource : ImageSource
    {
        protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource)
        {
            return new ImageSharpImageSourceImpl(name, () =>
            {
                return new Image(imageSource.Invoke());
            });
        }

        protected override IImageSource FromFileImpl(string path)
        {
            return new ImageSharpImageSourceImpl(path, () =>
            {
                return new Image(path);
            });
        }

        protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream)
        {
            return new ImageSharpImageSourceImpl(name, () =>
            {
                using (var stream = imageStream.Invoke())
                {
                    return new Image(stream);
                }
            });
        }

        private class ImageSharpImageSourceImpl : IImageSource
        {

            private Image _image;
            private Image Image
            {
                get
                {
                    if (_image == null)
                    {
                        _image = _getImage.Invoke();
                    }
                    return _image;
                }
            }
            private Func<Image> _getImage;
            public int Width => Image.Width;
            public int Height => Image.Height;
            public string Name { get; }

            public string FormatExtension => Image.CurrentImageFormat.Extension;

            public ImageSharpImageSourceImpl(string name, Func<Image> getImage)
            {
                Name = name;
                _getImage = getImage;
            }

            public void SaveAsJpeg(MemoryStream ms)
            {
                Image.AutoOrient();
                Image.SaveAsJpeg(ms);
            }

            public void Dispose()
            {
                Image.Dispose();
            }
        }
    }
}
