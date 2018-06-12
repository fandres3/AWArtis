using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AWArtis.Services;


namespace AWArtis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticusPage : ContentPage
	{
        private ArticusDataAccess dataAccess;
        private string _codigoArticulo;
        public ArticusPage (String codigoArticulo)
        {
			InitializeComponent ();
            _codigoArticulo = codigoArticulo;
            this.dataAccess = new ArticusDataAccess();
        }

        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CustomersDataAccess
            // is the data binding source
            //this.BindingContext = this.dataAccess.GetFilteredArticus();
            if (_codigoArticulo != null)
            {
                this.BindingContext = this.dataAccess.GetFilteredArticus(_codigoArticulo);
            }
            //this.BindingContext = this.dataAccess;
        }

    }
}