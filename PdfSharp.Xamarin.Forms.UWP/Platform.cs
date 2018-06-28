namespace PdfSharp.Xamarin.Forms.UWP
{
	public class Platform
	{
		public static void Init()
		{
			PDFManager.Init(new UwpImageSource());
		}
	}
}
