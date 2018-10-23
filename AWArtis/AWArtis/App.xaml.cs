using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AWArtis.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AWArtis
{
	public partial class App : Application
	{
      
        public App ()
		{
			InitializeComponent();

            IniciaSettings();

            //MainPage = new MainPage();
            //MainPage = new Views.ArticusPage();
            MainPage = new NavigationPage(new MainPage());
        }

        private void IniciaSettings()
        {
            if (!Application.Current.Properties.ContainsKey("CaminoAFichero"))
            {
                // Settings Generales
                Application.Current.Properties["CaminoAFichero"] = "/storage/emulated/0/AW/Gascon";
                Application.Current.Properties["Fichero"] = "AWBD1.DB3";
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
