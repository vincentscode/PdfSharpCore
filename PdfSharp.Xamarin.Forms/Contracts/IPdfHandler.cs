using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfSharp.Xamarin.Forms.Contracts
{
    public interface IPDFHandler
    {
        Stream GetImageStream(string imgName);
    }
}
