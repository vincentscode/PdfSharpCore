using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharp.Xamarin.Sample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PdfHandler))]
namespace PdfSharp.Xamarin.Sample.Droid
{
	class PdfHandler : IPDFHandler
	{
		public ImageSource GetImageSource()
		{
			return new AndroidImageSource();
		}
	}
}
