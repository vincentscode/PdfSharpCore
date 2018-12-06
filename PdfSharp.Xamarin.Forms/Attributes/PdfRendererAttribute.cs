using System;

namespace PdfSharp.Xamarin.Forms.Attributes
{
	internal class PdfRendererAttribute : Attribute
	{
		public Type ViewType { get; set; }
	}
}
