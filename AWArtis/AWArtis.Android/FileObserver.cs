using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace AWArtis.Droid
{
    public class MyPathObserver : Android.OS.FileObserver
    {
        static FileObserverEvents _Events = (FileObserverEvents.Modify);
        const string tag = "StackoverFlow";

        public MyPathObserver(String rootPath) : base(rootPath, _Events)
        {
            Log.Info(tag, String.Format("Watching : {0}", rootPath));
        }

        public MyPathObserver(String rootPath, FileObserverEvents events) : base(rootPath, events)
        {
            Log.Info(tag, String.Format("Watching : {0} : {1}", rootPath, events));
        }

        public override void OnEvent(FileObserverEvents e, String path)
        {
           // Log.Info(tag, String.Format("{0}:{1}", path, e));
        MessagingCenter.Send(this, "FechaBD", "33");

        }
    }
}