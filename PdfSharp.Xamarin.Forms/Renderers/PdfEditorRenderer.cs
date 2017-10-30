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
    [PdfRenderer(ViewType = typeof(Editor))]
    public class PdfEditorRenderer : PdfRendererBase<Editor>
    {
        public override void CreatePDFLayout(XGraphics page, Editor editor, XRect bounds, double scaleFactor)
        {
            if (editor.BackgroundColor != default(Color))
                page.DrawRectangle(editor.BackgroundColor.ToXBrush(), bounds);
            else
                page.DrawRoundedRectangle(new XPen(Color.LightGray.ToXColor(), 2), bounds, new XSize(2, 2));

			XFont font = new XFont(editor.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, editor.FontSize * scaleFactor);

            page.DrawString(editor.Text ?? "", font, editor.TextColor.ToXBrush(), bounds, 
                new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.BaseLine,
                });

        }
    }
}
