using PdfSharp.Xamarin.Forms;
using PdfSharp.Xamarin.Forms.Contracts;
using Xamarin.Forms;

namespace PdfSharp.Xamarin.Sample
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			PDFManager.Init(DependencyService.Get<IPDFHandler>());
			MainPage = new NavigationPage(new MainPage());
		}

		#region App Lifecycle
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		} 
		#endregion
	}
}
