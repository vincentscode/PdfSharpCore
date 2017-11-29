using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharp.Xamarin.Sample.Droid;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;

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
