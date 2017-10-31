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
    public partial class ScrollPage : ContentPage
    {
        public ScrollPage()
        {
            InitializeComponent();

            for(int i=0;i<250; i++)
            {
                Label l = new Label
                {
                    Text = $"Test Label {i}",
                    FontSize = 15,
                };

                contentLayout.Children.Add(l);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pdf = PdfSharp.Xamarin.Forms.PDFManager.GeneratePDFFromView(mainLayout);

            DependencyService.Get<IPdfSave>().Save(pdf, "ScrollPage.pdf");
        }
    }
}