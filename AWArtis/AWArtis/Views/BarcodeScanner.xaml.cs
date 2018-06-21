using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using AWArtis.Models;

// https://github.com/Redth/ZXing.Net.Mobile/issues/578

namespace AWArtis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BarcodeScanner : ContentPage
	{
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        public event EventHandler<string> BarcodeReaded;

        public BarcodeScanner ()
		{
			InitializeComponent ();
            GlobalVariables._codigoBarras = null;

            var options = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseFrontCameraIfAvailable = false,
                    TryHarder = true,
                    //PossibleFormats = new List<ZXing.BarcodeFormat>
                    //{
                    //    ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13, 
                    //    ZXing.BarcodeFormat.CODE_39,ZXing.BarcodeFormat.All_1D
                    //}
                };

                 zxing = new ZXingScannerView
                 {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Options = options
                 };

                zxing.OnScanResult += (result) =>
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    BarcodeReaded?.Invoke(this, result.Text);

                        // Navigate away
                        await Navigation.PopAsync();
                        // Envia mensaje con el código de barras capturado
                        MessagingCenter.Send<Views.BarcodeScanner, string>(this, "BarcodeRead", result.Text);


                        //if (result.Text != null)
                        //{
                        //    GlobalVariables._codigoBarras = result.Text;
                        //    // await Navigation.PushAsync(new Views.ArticusPage(result.Text, null));
                        //}


                    });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Sostenga su teléfono sobre el código de barras",
                BottomText = "El código se escaneará automaticamente",
                ShowFlashButton = true,
                    //ShowFlashButton = zxing.HasTorch,
                };

                overlay.FlashButtonClicked += (sender, e) =>
                {
                    zxing.IsTorchOn = !zxing.IsTorchOn;
                };

                var abort = new Button
                {
                    Text = "Cancelar",
                    VerticalOptions = LayoutOptions.End,
                    TextColor = Color.FromHex("#FFF"),
                    BackgroundColor = Color.FromHex("#4F51FF")
                };

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        abort.HeightRequest = 40;
                        break;
                    case Device.Android:
                        abort.HeightRequest = 50;
                        break;
                }

                abort.Clicked += (object s, EventArgs e) =>
                {
                    zxing.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                    });
                };

                var grid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                grid.Children.Add(zxing);
                grid.Children.Add(overlay);
                grid.Children.Add(abort);

                // The root page of your application
                Content = grid;
            }

    protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}
