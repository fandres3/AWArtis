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
using System.IO;
using AWArtis.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace AWArtis
{

    public partial class MainPage : ContentPage
    {
        private ArticusDataAccess dataAccess;
        private IEnumerable<Articu> SeleccionArticus;
        bool busy = false;

        public MainPage()
        {
            InitializeComponent();
        }

        async Task VerificaPermisos()
        {
            // ---- Basado en Plugin de permisos https://github.com/jamesmontemagno/PermissionsPlugin
            GlobalVariables._Permisos = false;
            // --------------------
            var statusStorage = PermissionStatus.Unknown;
            try
            {
                statusStorage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (statusStorage != PermissionStatus.Granted)
                {
                    //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    //{
                    //    await DisplayAlert("Necesito permiso de almacenamiento", "Necesito acceso a almacenamiento", "OK");
                    //}

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        statusStorage = results[Permission.Storage];
                    statusStorage = results[Permission.Storage];
                    GlobalVariables._Permisos = true;
                }

                if (statusStorage != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso almacenamiento denegado", "No puedo continuar, inténtalo de nuevo.", "OK");
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
            // --
            var statusCamera = PermissionStatus.Unknown;
            GlobalVariables._Permisos = false;
            try
            {
                statusCamera = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (statusCamera != PermissionStatus.Granted)
                {
                    //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    //{
                    //    await DisplayAlert("Necesito permiso para usar la cámara", "Necesito acceso a cámara", "OK");
                    //}

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        statusCamera = results[Permission.Camera];
                    statusCamera = results[Permission.Camera];
                    GlobalVariables._Permisos = true;
                }


                if (statusCamera != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso almacenamiento denegado", "No puedo continuar, inténtalo de nuevo.", "OK");
                    return;
                }
                else
                {
                    GlobalVariables._Permisos = true;
                }
            }
            catch (Exception)
            {
                return;
            }
            busy = true;


        }

        async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.ArticusPage(entryCodigo.Text, entryDescripcion.Text));

            if (busy == false)
            {
                await VerificaPermisos();
                if (GlobalVariables._Permisos == false) return;

                GlobalVariables._Camino = App.Current.Properties["CaminoAFichero"] as string;
                GlobalVariables._Fichero = App.Current.Properties["Fichero"] as string;
                GlobalVariables._FileName = Path.Combine(GlobalVariables._Camino, GlobalVariables._Fichero);

                if (!Directory.Exists(GlobalVariables._Camino))
                {
                    try
                    {
                        Directory.CreateDirectory(GlobalVariables._Camino);
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Aviso", "No puedo crear " + GlobalVariables._Camino, "OK");
                        return;
                    }

                }
                try
                {
                    dataAccess = new ArticusDataAccess();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Creando tabla Articus " + ex.Message, "OK");
                    return;
                }

                busy = true;
            }


            if (busy) await Buscar();
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



        async private void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender;
            if (toolbarItem.Text == "Configuración")
            {
                await Navigation.PushAsync(new SettingsGeneralPage());
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
            if (SeleccionArticus != null)
            {
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
            }
            return;

        }

    }
}


