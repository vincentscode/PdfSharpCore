using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PdfSharp.Xamarin.Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();
            List<User> users = new List<User>();
            for (int i = 0; i < 150; i++)
            {
                users.Add(new User
                {
                    ID = $"{i}",
                    Name = $"User {i}",
                });
            }

            list.ItemsSource = users;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pdf = PdfSharp.Xamarin.Forms.PDFManager.GeneratePDFFromView(mainGrid);

            DependencyService.Get<IPdfSave>().Save(pdf, "ListViewPage.pdf");
        }
    }

    public class PDFTestListViewRendererDelegate : PdfSharp.Xamarin.Forms.Delegates.PdfListViewRendererDelegate
    {
        public override void DrawCell(ListView listView, int section, int row, XGraphics page, XRect bounds, double scaleFactor)
        {
            base.DrawCell(listView, section, row, page, bounds, scaleFactor);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}