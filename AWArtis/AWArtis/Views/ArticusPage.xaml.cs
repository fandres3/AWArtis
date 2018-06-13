using AWArtis.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AWArtis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticusPage : ContentPage
	{
        private ArticusDataAccess dataAccess;
        private string _codigoArticulo;
        private string _descripcionArticulo;
        public ArticusPage (String codigoArticulo, String descripcionArticulo)
        {
			InitializeComponent ();
            _codigoArticulo = codigoArticulo;
            _descripcionArticulo = descripcionArticulo;
            this.dataAccess = new ArticusDataAccess();
        }

        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CustomersDataAccess
            // is the data binding source
            //this.BindingContext = this.dataAccess.GetFilteredArticus();
            if ((_codigoArticulo != null) || (_descripcionArticulo !=null))
            {
                // this.BindingContext = this.dataAccess.GetFilteredArticus(_codigoArticulo);
                ArticusView.ItemsSource = this.dataAccess.GetFilteredArticus(_codigoArticulo,_descripcionArticulo);
            }
           
            //this.BindingContext = this.dataAccess;
        }

    }
}