using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using PdfSharpCore.Drawing;
using PdfSharp.Xamarin.Forms.Attributes;
using System.IO;

namespace PdfSharp.Xamarin.Forms.Renderers
{
    [PdfRenderer(ViewType = typeof(Image))]
    public class PdfImageRenderer : PdfRendererBase<Image>
    {
        public override void CreatePDFLayout(XGraphics page, Image image, XRect bounds, double scaleFactor)
        {
            if (PDFManager.Instance.Handler == null)
                return;

            if (image.BackgroundColor != default(Color))
                page.DrawRectangle(image.BackgroundColor.ToXBrush(), bounds);

            if (image.Source == null)
                return;

            string imageSource = image.Source.ToString();
            if (imageSource.StartsWith("File: "))
                imageSource = imageSource.Replace("File: ", "").Replace(" ", "");

            XImage img = XImage.FromFile(imageSource);

            XRect desiredBounds = bounds;
            switch (image.Aspect)
            {
                case Aspect.Fill:
                    desiredBounds = bounds;
                    break;
                case Aspect.AspectFit:
                    {
                        double aspectRatio = ((double)img.PixelWidth) / img.PixelHeight;
                        desiredBounds = aspectRatio > bounds.Width / bounds.Height ? new XRect(bounds.X, bounds.Y, bounds.Width, bounds.Height / aspectRatio)
                                                                                   : new XRect(bounds.X, bounds.Y, bounds.Width, bounds.Width * aspectRatio);
                    }
                    break;
                //PdfSharp does not support drawing a portion pf image, its not supported 
                case Aspect.AspectFill:
                    desiredBounds = bounds;
                    break;
            }

            page.DrawImage(img, desiredBounds, new System.Threading.CancellationToken());
        }

    }
}
