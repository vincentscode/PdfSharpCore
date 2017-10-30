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
    [PdfRenderer(ViewType = typeof(ProgressBar))]
    public class PdfProgressBarRenderer : PdfRendererBase<ProgressBar>
	{
		public override void CreatePDFLayout(XGraphics page, ProgressBar progressBar, XRect bounds, double scaleFactor)
		{
			Color bgColor = progressBar.BackgroundColor;
			if (bgColor == default(Color))
				bgColor = Color.White;

			Color barColor = Color.LightBlue;


			page.DrawRectangle(bgColor.ToXBrush(), bounds);


			XRect progress = new XRect(bounds.X + scaleFactor,
									   bounds.Y + scaleFactor,
									   bounds.Width * progressBar.Progress,
									   bounds.Height - 2 * scaleFactor);

			page.DrawRectangle(barColor.ToXBrush(), progress);

			//Draw borders
			page.DrawRoundedRectangle(new XPen(Color.LightGray.ToXColor(), 2 * scaleFactor), bounds, new XSize(3 * scaleFactor, 3 * scaleFactor));
		}
	}
}
