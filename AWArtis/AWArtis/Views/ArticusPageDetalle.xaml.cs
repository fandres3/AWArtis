using AWArtis.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AWArtis.Models;
using System.Collections.Generic;


namespace AWArtis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticusPageDetalle : ContentPage
    {
        private IEnumerable<Articu> _seleccionArticus;
        private string v;

        public ArticusPageDetalle(IEnumerable<Articu> SeleccionArticus)
        {
            InitializeComponent();
            _seleccionArticus = SeleccionArticus;
            this.BindingContext = _seleccionArticus;
            ArticusViewDetalle.ItemsSource = _seleccionArticus;
            //this.dataAccess = new ArticusDataAccess();
        }

        public ArticusPageDetalle(string v)
        {
            this.v = v;
        }

        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = _seleccionArticus;
            ArticusViewDetalle.ItemsSource = _seleccionArticus;


        }

       
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
        }

    }
}