using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using AWArtis.Models;

namespace AWArtis
{

    public partial class MainPage : ContentPage
	{

        async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
        }
        async void btnPrueba_Clicked(object sender, EventArgs e)
        {
            entryCodigo.Text = "";
            entryDescripcion.Text = "";
            await Navigation.PushAsync(new Views.BarcodeScanner());
          
        }

        async void btnLeerCodigo_Clicked(object sender, EventArgs e)
        {
            entryCodigo.Text = null;
            entryDescripcion.Text = null;
            // ---- Información ZXing
            // http://slackshotindustries.blogspot.com/2013/04/creating-custom-overlays-in-xzing.html
            // https://github.com/icebeam7/ScannerZXing
            // https://github.com/Redth/ZXing.Net.Mobile/blob/master/readme.md
            // ----

            var overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = true,
                BindingContext = this,
                //TopText = "I'm at the top",
                // BottomText = "I'm at the bottom",
                
            };


            //'overlay.BindingContext = overlay;
            var pagina = new ZXingScannerPage(null, overlay);
            

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
        }


        public MainPage()
		{
			InitializeComponent();


        
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
