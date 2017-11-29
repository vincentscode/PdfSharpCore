using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharp.Xamarin.Forms.Contracts;
using PdfSharp.Xamarin.Sample.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(PdfHandler))]
namespace PdfSharp.Xamarin.Sample.iOS
{
    class PdfHandler : IPDFHandler
    {
        public ImageSource GetImageSource()
        {
            return new IosImageSource();
        }
    }
}
