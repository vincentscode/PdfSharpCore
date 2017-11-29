using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PdfSharp.Xamarin.Forms;

using Xamarin.Forms;

namespace PdfSharp.Xamarin.Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			picker.ItemsSource = new List<string>() { "Item 1", "Item 2", "Item 3" };
			picker.SelectedIndex = 0;
		}

		private void GeneratePDF(object sender, EventArgs e)
		{
			var pdf = PDFManager.GeneratePDFFromView(mainGrid);

			DependencyService.Get<IPdfSave>().Save(pdf, "SinglePage.pdf");
		}
	}
}
