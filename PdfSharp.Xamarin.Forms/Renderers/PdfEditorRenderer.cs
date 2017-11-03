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
			Color textColor = editor.TextColor != default(Color) ? editor.TextColor : Color.Black;
			XFont font = new XFont(editor.FontFamily ?? GlobalFontSettings.FontResolver.DefaultFontName, editor.FontSize * scaleFactor);

			if (editor.BackgroundColor != default(Color))
				page.DrawRectangle(editor.BackgroundColor.ToXBrush(), bounds);

			//Border
			page.DrawRectangle(new XPen(Color.Gray.ToXColor(), 2 * scaleFactor), bounds);

			if (!string.IsNullOrEmpty(editor.Text))
				page.DrawString(editor.Text, font, textColor.ToXBrush(), bounds.AddMargin(5 * scaleFactor),
					new XStringFormat
					{
						Alignment = XStringAlignment.Near,
						LineAlignment = XLineAlignment.Near,
					});

		}
	}
}
