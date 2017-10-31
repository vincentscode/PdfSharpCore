using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PdfSharp.Xamarin.Forms.PDFManager.Init(null, null);
        }
    }
}
