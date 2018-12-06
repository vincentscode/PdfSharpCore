namespace PdfSharp.Xamarin.Forms.Contracts
{
	public interface ICustomFontProvider
	{
		byte[] GetFont(string faceName);

		string ProvideFont(string fontName, bool isItalic, bool isBold);
	}
}
