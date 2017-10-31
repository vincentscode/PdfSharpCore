using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PdfSharp.Xamarin.Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SinglePage : ContentPage
    {
        public SinglePage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pdf = PdfSharp.Xamarin.Forms.PDFManager.GeneratePDFFromView(mainGrid);

            DependencyService.Get<IPdfSave>().Save(pdf, "SinglePage1.pdf");
        }
    }
}