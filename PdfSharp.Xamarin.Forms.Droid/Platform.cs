using PdfSharp.Xamarin.Forms.Contracts;

namespace PdfSharp.Xamarin.Forms.Droid
{
	public class Platform
	{
		public static void Init(ICustomFontProvider customFontProvider=null)
		{
			PDFManager.Init(new AndroidImageSource(),customFontProvider);
		}
	}
}