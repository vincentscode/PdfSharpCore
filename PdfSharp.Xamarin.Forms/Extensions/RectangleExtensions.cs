using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using PdfSharpCore.Drawing;

namespace PdfSharp.Xamarin.Forms
{
	public static class RectangleExtensions
	{
		public static XRect ToXRect(this Rectangle rect)
		{
			return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
		}
	}
}
