namespace PdfSharp.Xamarin.Forms.Droid
{
	public class Platform
	{
		public static void Init()
		{
			PDFManager.Init(new AndroidImageSource());
		}
	}
}