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
using AWArtis.Services;

namespace AWArtis
{

    public partial class MainPage : ContentPage
    {
        private ArticusDataAccess dataAccess;
        private IEnumerable<Articu> SeleccionArticus;

        void btnBuscar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
            Buscar();
        }

        async void btnLeerCodigo_Clicked(object sender, EventArgs e)
        {

            entryCodigo.Text = "";
            entryDescripcion.Text = "";
            GlobalVariables._IsBusy = false;
            await Navigation.PushAsync(new Views.BarcodeScanner());
            // Vuelve a aquí antes de haber leído el barcode
            // Para evitar retornos, una vez que BarcodeScanner lee el barcode y haber PopAsync de su Page
            // Lanza un    MessagingCenter.Send<Views.BarcodeScanner, string>(this, "BarcodeRead", result.Text);
            // Esto hace que MainPage en OnAppearing recoja el barcode con
            //            MessagingCenter.Subscribe<Views.BarcodeScanner, string>(this, "BarcodeRead", (sender, arg) => ....
        }

        void btnBorraCodigo_Clicked(object sender, EventArgs e)
        {
            entryCodigo.Text = "";
        }

        void btnBorraDescripcion_Clicked(object sender, EventArgs e)
        {
            entryDescripcion.Text = "";
        }


        public MainPage()
        {
            InitializeComponent();
            dataAccess = new ArticusDataAccess();
        }

        async private void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Configuración")
            {
                await Navigation.PushAsync(new Views.ArticusPage("", ""));
            }
        }

        async private void OnCodigoCompleted(object sender, TextChangedEventArgs args)
        {
            entryDescripcion.Text = "";
            //await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
            await Buscar();
        }

        async private void OnDescripcionCompleted(object sender, TextChangedEventArgs args)
        {
            entryCodigo.Text = "";
            //await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
            await Buscar();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Suscrito a mensaje de Barcode que lo envía cuando ha leído un código de barras.
            MessagingCenter.Subscribe<Views.BarcodeScanner, string>(this, "BarcodeRead", (sender, arg) =>
            {
                // arg should have your barcode...
                if (arg != null)
                {
                    zz(arg);
                    arg = null;
                }
            });
        }

        async private void zz(String barcode)
        {
            if (GlobalVariables._IsBusy) return; // Evita que se lance varias veces ArticusPage
            GlobalVariables._IsBusy = true;
            entryCodigo.Text = barcode;
            entryDescripcion.Text = "";
            //await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));
            await Buscar();

        }

        // Busca el código
        // Busca por codigo o descripción 
        async private Task Buscar()
        {

            SeleccionArticus = dataAccess.GetFilteredArticus(entryCodigo.Text, entryDescripcion.Text);
            var z = SeleccionArticus.Count();
            if (z == 1)
            {
                var vi = new Views.ArticusPageDetalle(SeleccionArticus)
                {
                    BindingContext = SeleccionArticus
                };
                await Navigation.PushAsync(vi);
                return;
            }

            if (z > 0)
            {
                var vi = new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text)
                {
                    BindingContext = SeleccionArticus
                };
                await Navigation.PushAsync(vi);
                return;
            }

            if (z == 0)
            {
                await DisplayAlert("Aviso", "No existe", "OK");
            }
            return;
        }

}
}


