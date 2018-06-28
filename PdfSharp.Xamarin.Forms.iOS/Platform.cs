namespace PdfSharp.Xamarin.Forms.iOS
{
	public class Platform
	{
		public static void Init()
		{
			PDFManager.Init(new IosImageSource());
		}
	}
}