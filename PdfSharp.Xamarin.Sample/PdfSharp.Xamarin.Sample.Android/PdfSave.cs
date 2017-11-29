using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Pdf;

using PdfSharp.Xamarin.Sample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PdfSave))]
namespace PdfSharp.Xamarin.Sample.Droid
{
	public class PdfSave : IPdfSave
	{
		public void Save(PdfDocument doc, string fileName)
		{
			string path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + fileName);

			doc.Save(path);
			doc.Close();

			global::Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
				title: "Success",
				message: $"Your PDF generated and saved @ {path}",
				cancel: "OK");
		}
	}
}
