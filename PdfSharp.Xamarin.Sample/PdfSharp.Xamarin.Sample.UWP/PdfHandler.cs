using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Xamarin.Forms.Contracts;
using Windows.Storage;
using PdfSharp.Xamarin.Sample.UWP;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;

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
