using PdfSharp.Xamarin.Sample.UWP;
using PdfSharpCore.Pdf;

[assembly: Xamarin.Forms.Dependency(typeof(PdfSave))]
namespace PdfSharp.Xamarin.Sample.UWP
{
	public class PdfSave : IPdfSave
	{
		public void Save(PdfDocument doc, string fileName)
		{
			string path = System.IO.Path.GetTempPath() + fileName;

			doc.Save(System.IO.Path.GetTempPath() + fileName);

			global::Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
				title: "Success",
				message: $"Your PDF generated and saved @ {path}",
				cancel: "OK");
		}
	}
}
