using ImageSharp;
using MigraDoc.DocumentObjectModel.MigraDoc.DocumentObjectModel.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MigraDoc.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes
{
    public abstract class ImageSource
    {
        public static ImageSource FromFile(string path)
        {
            return new ImageFileSource(path);
        }

        public static ImageSource FromImage(string name, Func<Image> imageSource)
        {
            return new ImageImageSource(name, imageSource);
        }

        public static ImageSource FromBinary(string name, Func<byte[]> imageSource)
        {
            return new ImageBinarySource(name, imageSource);
        }

        public static ImageSource FromStream(string name, Func<Stream> imageSource)
        {
            return new ImageStreamSource(name, imageSource);
        }

        private Image _image;
        public Image Image
        {
            get
            {
                if (_image == null)
                    _image = InstantiateImage();
                return _image;
            }
        }

        protected abstract Image InstantiateImage();

        public override string ToString()
        {
            return string.Format(AppResources.ImageTypeString, GetType().Name);
        }

        internal abstract string Name { get; }

        public class ImageFileSource : ImageSource
        {
            public string Path { get; }

            internal override string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

            public ImageFileSource(string path)
            {
                Path = path;
            }

            protected override Image InstantiateImage()
            {
                var image = new Image(Path);
                image.AutoOrient();
                return image;
            }

            public override string ToString()
            {
                return string.Format(AppResources.ImagePathString, Path);
            }
        }

        private abstract class ImageLazySource : ImageSource
        {
            internal override string Name { get; }
            public ImageLazySource(string name)
            {
                Name = name;
            }
        }

        private class ImageImageSource : ImageLazySource
        {
            private Func<Image> _imageSource;

            public ImageImageSource(string name, Func<Image> imageSource) : base(name)
            {
                _imageSource = imageSource;
            }

            protected override Image InstantiateImage()
            {
                var image = new Image(_imageSource.Invoke());
                image.AutoOrient();
                return image;
            }
        }

        private class ImageBinarySource : ImageLazySource
        {
            private Func<byte[]> _imageSource;

            public ImageBinarySource(string name, Func<byte[]> imageSource) : base(name)
            {
                _imageSource = imageSource;
            }

            protected override Image InstantiateImage()
            {
                var image = new Image(_imageSource.Invoke());
                image.AutoOrient();
                return image;
            }
        }

        private class ImageStreamSource : ImageLazySource
        {
            private Func<Stream> _imageSource;

            public ImageStreamSource(string name, Func<Stream> imageSource) : base(name)
            {
                _imageSource = imageSource;
            }

            protected override Image InstantiateImage()
            {
                using (var stream = _imageSource.Invoke())
                {
                    var image = new Image(stream);
                    image.AutoOrient();
                    return image;
                }
            }
        }
    }
}
