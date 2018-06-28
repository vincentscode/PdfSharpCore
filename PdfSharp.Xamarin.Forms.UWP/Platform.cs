using PdfSharp.Xamarin.Forms.Contracts;

namespace PdfSharp.Xamarin.Forms.UWP
{
	public class Platform
	{
		public static void Init(ICustomFontProvider customFontProvider = null)
		{
			PDFManager.Init(new UwpImageSource(), customFontProvider);
		}
	}
}
