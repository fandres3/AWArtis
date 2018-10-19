using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.IO;
using Java.Util;

namespace AWArtis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsGeneralPage : ContentPage
    {

        // byte[] buffer;

        public SettingsGeneralPage()
        {
            InitializeComponent();
            CargaSettings();
        }

        void CargaSettings()
        {
            if (Application.Current.Properties.ContainsKey("CaminoAFichero"))
                txtCaminoAFichero.Text = Application.Current.Properties["CaminoAFichero"] as string;
            if (Application.Current.Properties.ContainsKey("Fichero"))
                txtFichero.Text = Application.Current.Properties["Fichero"] as string;

        }

        void GrabaSettings(object sender, EventArgs args)
        {
            Application.Current.Properties["CaminoAFichero"] = txtCaminoAFichero.Text;
            Application.Current.Properties["Fichero"] = txtFichero.Text;
            App.Current.SavePropertiesAsync();
        }

        void BtnPruebaImpresionClicked(object sender, EventArgs args)
        {

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

    }
}