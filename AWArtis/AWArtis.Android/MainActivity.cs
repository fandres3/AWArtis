using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;


[assembly: UsesPermission(Android.Manifest.Permission.Flashlight)]

namespace AWArtis.Droid
{
    [Activity(Label = "AWArtis", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication(new App());
        }

        // https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-data-in-an-app
        //var docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        // Console.WriteLine("Data path:" + Database.DatabaseFilePath);
        //var dbFile = Path.Combine(docFolder, "AWBD3.db"); // FILE NAME TO USE WHEN COPIED
        //if (!System.IO.File.Exists(dbFile))
        // {
        //   var s = Resources.OpenRawResource(Resource.Raw.AWBD1);  // DATA FILE RESOURCE ID
        //   FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
        //   ReadWriteStream(s, writeStream);
        // }
      
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, 
            Permission[] grantResults) {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode,
                permissions, grantResults);
        }

    }

        // readStream is the stream you need to read
        // writeStream is the stream you want to write to
        //private void ReadWriteStream(Stream readStream, Stream writeStream)
       // {
         //   int Length = 256;
          //  Byte[] buffer = new Byte[Length];
          //  int bytesRead = readStream.Read(buffer, 0, Length);
          //  // write the required bytes
          //  while (bytesRead > 0)
           // {
        //        writeStream.Write(buffer, 0, bytesRead);
        //        bytesRead = readStream.Read(buffer, 0, Length);
        //    }
        //    readStream.Close();
        //    writeStream.Close();
        //}
    }


