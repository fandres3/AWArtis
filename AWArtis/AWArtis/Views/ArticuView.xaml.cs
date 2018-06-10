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
    public partial class ArticuView : ContentPage
	{
        private ArticusDataAccess dataAccess;

        public ArticuView ()
        {
			InitializeComponent ();
            this.dataAccess = new ArticusDataAccess();
        }
        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of CustomersDataAccess
            // is the data binding source
            
            this.BindingContext = this.dataAccess.GetFilteredArticus();
        }

    }
}