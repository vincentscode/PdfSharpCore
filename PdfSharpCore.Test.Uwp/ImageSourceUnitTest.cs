
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharpCore.Uwp;
using PdfSharpCore.Pdf;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.Rendering;
using System.IO;
using System.Reflection;
using Windows.Storage;
using System.Linq;
using PdfSharpCore.Fonts;
using static MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharpCore.Test.Uwp
{
    [TestClass]
    public class ImageSourceUnitTest
    {
        [TestInitialize()]
        public void TestSetup()
        {
            ImageSource.ImageSourceImpl = new UwpImageSource();
            GlobalFontSettings.FontResolver = new FontResolverDummy();
        }

        private class FontResolverDummy : IFontResolver
        {
            public string DefaultFontName => string.Empty;

            public byte[] GetFont(string faceName)
            {
                return null;
            }

            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                return null;
            }
        }

        [TestMethod]
        public void TestFileImageSource()
        {
            var image = ImageSource.FromFile("Assets/TestImage.png");
            AssertImageSource(image);
        }

        [TestMethod]
        public void TestBinaryImageSource()
        {
            var image = ImageSource.FromBinary("TestImage.png", () =>
            {

                using (var fs = File.OpenRead("Assets/TestImage.png"))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        return br.ReadBytes((int)fs.Length);
                    }
                }
            });
            AssertImageSource(image);
        }

        [TestMethod]
        public void TestStreamImageSource()
        {
            var image = ImageSource.FromStream("TestImage.png", () =>
            {
               return File.OpenRead("Assets/TestImage.png");
            });
            AssertImageSource(image);
        }

        private void AssertImageSource(IImageSource image)
        {
            var pdf = new PdfDocument();
            var doc = new Document();
            var sec = doc.AddSection();

            sec.AddImage(image);

            PdfDocumentRenderer renderer = new PdfDocumentRenderer()
            {
                Document = doc,
                PdfDocument = pdf
            };
            renderer.RenderDocument();

            using (var ms = new MemoryStream())
            {
                pdf.Save(ms);
                Assert.AreEqual(5392, ms.Length);
            }
        }
    }
}
