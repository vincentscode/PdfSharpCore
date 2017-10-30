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
			//Draw background Color
			if (entry.BackgroundColor != default(Color))
				page.DrawRectangle(entry.BackgroundColor.ToXBrush(), bounds);

			page.DrawRoundedRectangle(new XPen(Color.Gray.ToXColor(), 3 * scaleFactor), bounds, new XSize(3 * scaleFactor, 3 * scaleFactor));

			XFont font = new XFont(entry.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, entry.FontSize * scaleFactor);

			Color textColor = entry.TextColor;
			if (textColor == default(Color))
				textColor = Color.Black;

			page.DrawString(entry.Text ?? "", font, textColor.ToXBrush(), bounds,
				new XStringFormat()
				{
					Alignment = entry.HorizontalOptions.ToXStringAlignment(),
					LineAlignment = (XLineAlignment)entry.HorizontalTextAlignment
				});

		}
	}
}
