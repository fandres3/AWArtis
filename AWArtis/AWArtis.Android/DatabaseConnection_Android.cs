using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AWArtis.Droid;
using AWArtis.Services;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace AWArtis.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "AWBD1.db3";
           // var path = Path.Combine(System.Environment.
            //  GetFolderPath(System.Environment.
             // SpecialFolder.MyDocuments), dbName);

            var path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, dbName);
            try
            {
                return new SQLiteConnection(path);
            }
            catch
            {
                return null;
            }

        }
    }
}