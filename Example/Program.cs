using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var doc = new PdfDocument("test.pdf");
            var page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            gfx.MUH = PdfFontEncoding.Unicode;

            XFont font1 = new XFont("Tinos", 14, XFontStyle.Regular);
            


            using(var file = File.OpenWrite("test.pdf"))
            {

            }*/

            GlobalFontSettings.FontResolver = new FontResolver();

            var pdf = new PdfDocument();
            var doc = new Document();
            var sec = doc.AddSection();

            var table = sec.AddTable();
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Top.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Borders.Bottom.Width = 0.5;
            table.AddColumn(100);
            table.AddColumn(100);
            table.AddRow();
            table.AddRow();

            var row = table.AddRow();
            row.Cells[0].AddParagraph("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            row.Cells[1].AddParagraph("bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb bbbb");
            row = table.AddRow();
            row.Cells[0].AddParagraph("cccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc");
            row.Cells[1].AddParagraph("dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd dddd");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer()
            {
                Document = doc,
                PdfDocument = pdf
            };
            renderer.RenderDocument();

            using (var stream = File.OpenWrite("test.pdf"))
                pdf.Save(stream);
        }
    }
    class FontResolver : IFontResolver
    {
        public string DefaultFontName => "Tinos";

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                var assembly = typeof(FontResolver).GetTypeInfo().Assembly;
                var resourceName = assembly.GetManifestResourceNames().First(x => x.EndsWith(faceName));
                using (var rs = assembly.GetManifestResourceStream(resourceName))
                {
                    rs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Tinos", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("Tinos-BoldItalic.ttf");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("Tinos-Bold.ttf");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("Tinos-Italic.ttf");
                }
                else
                {
                    return new FontResolverInfo("Tinos-Regular.ttf");
                }
            }
            return null;
        }
    }

}