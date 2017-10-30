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
    [PdfRenderer(ViewType = typeof(Button))]
    public class PdfButtonRenderer : PdfRendererBase<Button>
	{
		public static readonly XStringFormat DefaultTextFormat = new XStringFormat
		{
			LineAlignment = XLineAlignment.Center,
			Alignment = XStringAlignment.Center,
		};

		public override void CreatePDFLayout(XGraphics page, Button button, XRect bounds, double scaleFactor)
		{
			if (button.BackgroundColor != default(Color))
				page.DrawRectangle(button.BackgroundColor.ToXBrush(), bounds);
			if (button.BorderWidth > 0 && button.BorderColor != default(Color))
				page.DrawRectangle(new XPen(button.BorderColor.ToXColor(), button.BorderWidth * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(button.Text))
			{
				XFont font = new XFont(button.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, button.FontSize * scaleFactor);
				Color textColor = button.TextColor;
				if (textColor == default(Color))
					textColor = Color.Black;

				page.DrawString(button.Text, font, textColor.ToXBrush(), bounds, DefaultTextFormat);
			}
		}
	}
}
