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
        }

        private void SinglePageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SinglePage());
        }

        private void ScrollPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScrollPage());
        }
        private void ListViewPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewPage());
        }
    }
}
