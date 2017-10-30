using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using PdfSharpCore.Drawing;

namespace PdfSharp.Xamarin.Forms
{
	public static class LayoutOptionsExtension
	{
		public static XStringAlignment ToXStringAlignment(this LayoutOptions lOpt)
		{
			switch (lOpt.Alignment)
			{
				case LayoutAlignment.Start:
					return XStringAlignment.Near;
				case LayoutAlignment.Center:
					return XStringAlignment.Center;
				case LayoutAlignment.End:
					return XStringAlignment.Far;
				default:
					return XStringAlignment.Near;
			}
		}
	}
}
