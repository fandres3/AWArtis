using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWArtis.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AWArtis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Page1 : ContentPage
	{
        Articu arti1 = new Articu { Art_cod = "11111", Art_des = "descri 111111", Art_preven1 = 89.10 };
        Articu arti2 = new Articu { Art_cod = "2222222", Art_des = "descri 2222", Art_preven1 = 22.10 };
        Articu arti3 = new Articu { Art_cod = "33333", Art_des = "descri 33333", Art_preven1 = 33.10 };
       

        public Page1 ()
		{
			InitializeComponent ();
            var Articulos = new ObservableCollection<Articu>() { arti1, arti2, arti3 };
            this.BindingContext = Articulos;
            this.ArticuPicker.ItemsSource = Articulos;
        }

        string databasePath
        {
            get
            {
                var dbName = "ItemsSQLite.db3";
#if __IOS__
string folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
folder = Path.Combine (folder, "..", "Library");
var databasePath = Path.Combine(folder, dbName);
#else
#if __ANDROID__
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var databasePath = Path.Combine(folder, dbName);
#else
// WinPhone
v ar databasePath = Path.Combine(Windows.Storage.ApplicationData.Current.
LocalFolder.Path, dbName);;
#endif
#endif
                return databasePath;
            }
        }

     

      
   
}
}