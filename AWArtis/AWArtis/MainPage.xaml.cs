using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AWArtis
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BtnBuscar.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Views.Page1());
            };

            BtnLeerCodigo.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Views.Page1());
            };

        }

      
    }
}
