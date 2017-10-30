using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using PdfSharpCore.Fonts;
using PdfSharpCore.Drawing;
using PdfSharp.Xamarin.Forms.Attributes;

namespace PdfSharp.Xamarin.Forms.Renderers
{
    [PdfRenderer(ViewType = typeof(Label))]
    public class PDFLabelRenderer : PdfRendererBase<Label>
    {
        public override void CreatePDFLayout(XGraphics page, Label label, XRect bounds, double scaleFactor)
        {
            if (label.BackgroundColor != default(Color))
                page.DrawRectangle(label.BackgroundColor.ToXBrush(), bounds);

            XFont font = new XFont(label.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, label.FontSize * scaleFactor);
            Color textColor = label.TextColor;

            if (textColor == default(Color))
                textColor = Color.Black;

            page.DrawString(label.Text, font, textColor.ToXBrush(), bounds,
                new XStringFormat()
                {
                    Alignment = label.HorizontalOptions.ToXStringAlignment(),
                    LineAlignment = (XLineAlignment)label.HorizontalTextAlignment
                });
        }
    }
}
