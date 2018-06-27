using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharp.Xamarin.Sample.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(PdfHandler))]
namespace PdfSharp.Xamarin.Sample.UWP
{
	class PdfHandler : IPDFHandler
	{
		public ImageSource GetImageSource()
		{
			return new UwpImageSource();
		}
	}
}
