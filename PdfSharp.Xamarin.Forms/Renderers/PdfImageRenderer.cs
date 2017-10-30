using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using PdfSharpCore.Drawing;
using PdfSharp.Xamarin.Forms.Attributes;

namespace PdfSharp.Xamarin.Forms.Renderers
{
    [PdfRenderer(ViewType = typeof(Image))]
    public class PdfImageRenderer : PdfRendererBase<Image>
    {
        public override void CreatePDFLayout(XGraphics page, Image image, XRect bounds, double scaleFactor)
        {
            if (PDFManager.Instance.Handler == null)
                return;

            XImage img = XImage.FromStream(() => { return PDFManager.Instance.Handler.GetImageStream(image.Source.ToString()); });

            page.DrawImage(img, bounds, new System.Threading.CancellationToken());

        }
    }
}
