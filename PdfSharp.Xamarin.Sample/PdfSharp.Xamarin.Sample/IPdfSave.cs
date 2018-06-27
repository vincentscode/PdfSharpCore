using PdfSharpCore.Pdf;

namespace PdfSharp.Xamarin.Sample
{
	public interface IPdfSave
	{
		void Save(PdfDocument doc, string fileName);
	}
}
