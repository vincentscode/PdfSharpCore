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
    [PdfRenderer(ViewType = typeof(Entry))]
    public class PdfEntryRenderer : PdfRendererBase<Entry>
    {
        public override void CreatePDFLayout(XGraphics page, Entry entry, XRect bounds, double scaleFactor)
        {
            Color textColor = entry.TextColor != default(Color) ? entry.TextColor : Color.Black;
            XFont font = new XFont(entry.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, entry.FontSize * scaleFactor);

            if (entry.BackgroundColor != default(Color))
                page.DrawRectangle(entry.BackgroundColor.ToXBrush(), bounds);

            // Border
            page.DrawRectangle(new XPen(Color.LightGray.ToXColor(), 1.5 * scaleFactor), bounds);

            if (!string.IsNullOrEmpty(entry.Text))
            {
                page.DrawString(entry.Text, font, textColor.ToXBrush(), bounds.AddMargin(5 * scaleFactor),
                    new XStringFormat()
                    {
                        Alignment = entry.HorizontalOptions.ToXStringAlignment(),
                        LineAlignment = (XLineAlignment)entry.HorizontalTextAlignment
                    });
            }
        }
    }
}
