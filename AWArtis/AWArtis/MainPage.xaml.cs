using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace AWArtis
{
	public partial class MainPage : ContentPage
	{
       
        public MainPage()
		{
			InitializeComponent();

            BtnBuscar.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text,entryDescripcion.Text));
            };

            BtnLeerCodigo.Clicked += async (sender, args) =>
            {
                entryCodigo.Text = null;
                entryDescripcion.Text=null;
                // http://slackshotindustries.blogspot.com/2013/04/creating-custom-overlays-in-xzing.html
                var overlay = new ZXingDefaultOverlay
                {
                    ShowFlashButton = true,
                    BindingContext = this,
                   //TopText = "I'm at the top",
                   // BottomText = "I'm at the bottom",

                };
                //'overlay.BindingContext = overlay;
                var pagina = new ZXingScannerPage(null,overlay);

                await Navigation.PushAsync(pagina);

                pagina.OnScanResult += (resultado) =>
                {
                    pagina.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        entryCodigo.Text = resultado.Text;
                        if (resultado.Text != null)
                        {
                            await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
                        }
                    });
                };

            };

        }

        async private void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Configuración") {
          //      await Navigation.PushAsync(new Views.Page1());
            }
        }

        async private void OnCodigoCompleted(object sender, TextChangedEventArgs args)
        {
            entryDescripcion.Text = "";
            await Navigation.PushAsync(new Views.ArticusPage ( entryCodigo.Text, entryDescripcion.Text));
        }

        async private void OnDescripcionCompleted(object sender, TextChangedEventArgs args)
        {
            entryCodigo.Text = "";
            await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));

        }


    }
}
