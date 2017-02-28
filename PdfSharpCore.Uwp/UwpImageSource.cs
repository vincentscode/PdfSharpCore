using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using Windows.Foundation;

namespace PdfSharpCore.Uwp
{
    public class UwpImageSource : ImageSource
    {
        protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
        {
            return new UwpImageSourceImpl(name,
                () =>
                {
                    return new MemoryStream(imageSource.Invoke()).AsRandomAccessStream();
                }, (int)quality);
        }

        protected override IImageSource FromFileImpl(string path, int? quality = 75)
        {
            return new UwpImageSourceImpl(Path.GetFileName(path),
                () =>
                {
                    return Task.Run(async () =>
                    {
                        var file = await StorageFile.GetFileFromPathAsync(path);
                        return await file.OpenReadAsync();
                    }).Result;
                }, (int)quality);
        }

        protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
        {
            return new UwpImageSourceImpl(name,
                () =>
                {
                    return imageStream.Invoke().AsRandomAccessStream();
                }, (int)quality);
        }

        private class UwpImageSourceImpl : IImageSource
        {
            private readonly int _quality;
            private readonly Func<IRandomAccessStream> _getRas;

            public int Width { get; }

            public int Height { get; }

            public string Name { get; }

            public UwpImageSourceImpl(string name, Func<IRandomAccessStream> getRas, int quality)
            {
                Name = name;
                _getRas = getRas;
                _quality = quality;
                using (var ras = getRas.Invoke())
                {
                    var decoder = GetDecoder(ras);
                    Width = (int)decoder.OrientedPixelWidth;
                    Height = (int)decoder.OrientedPixelHeight;
                }
            }

            private BitmapDecoder GetDecoder(IRandomAccessStream ras)
            {
                return Task.Run(async () => { return await BitmapDecoder.CreateAsync(ras); }).Result;
            }

            public void SaveAsJpeg(MemoryStream ms)
            {
                using (var ras = _getRas.Invoke())
                {
                    var decoder = GetDecoder(ras);
                    var encoder = BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, ms.AsRandomAccessStream(),
                        new BitmapPropertySet() {
                             { "ImageQuality", new BitmapTypedValue(Convert.ToSingle(_quality) / 100, PropertyType.Single) }
                        }).Await();
                    encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, decoder.OrientedPixelWidth, decoder.OrientedPixelHeight, decoder.DpiX, decoder.DpiY, decoder.GetPixelDataAsync().Await().DetachPixelData());
                    var properties = decoder.BitmapProperties.GetPropertiesAsync(new List<string> { "System.Photo.Orientation" }).Await();
                    if (properties.TryGetValue("System.Photo.Orientation", out BitmapTypedValue value))
                    {
                        //ApplyOrientation(encoder, value);
                    }
                    encoder.FlushAsync().Await();
                }
            }
        }
    }

    internal static class TaskHelper
    {

        internal static T Await<T>(this Task<T> task)
        {
            return Task.Run(async () => { return await task; }).Result;
        }
        internal static T Await<T>(this IAsyncOperation<T> task)
        {
            return Task.Run(async () => { return await task; }).Result;
        }
        internal static void Await(this IAsyncAction task)
        {
            var dummy = Task.Run(async () => { await task; return true; }).Result;
        }
    }
}
