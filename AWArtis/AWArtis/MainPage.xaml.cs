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
        bool result;

        public MainPage()
		{
			InitializeComponent();

            BtnBuscar.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text));
            };

            BtnLeerCodigo.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Views.Page1());
            };

        }

        async private void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Configuración") {
                await Navigation.PushAsync(new Views.Page1());
            }
        }

        async private void OnCodigoCompleted(object sender, TextChangedEventArgs args)
        {
             await Navigation.PushAsync(new Views.ArticusPage ( entryCodigo.Text));
           
        }


    }
}
