using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfSharp.Xamarin.Sample
{
    public interface IPdfSave
    {
        void Save(PdfDocument doc,string fileName);
    }
}
