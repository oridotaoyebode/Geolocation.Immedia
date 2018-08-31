using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;

namespace Immedia.Demo.Geolocation.Helpers
{
    public abstract class BaseActivity: AppCompatActivity
    {

        public static BaseActivity CurrentActivity { get; private set; }

        internal string ActivityKey { get; private set; }

        internal static string NextPageKey { get; set; }

        public static void GoBack()
        {
            CurrentActivity?.OnBackPressed();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //SetActionBar(toolbar);
            //ActionBar.Title = "Hello from Toolbar";
        }

        protected override void OnResume()
        {
            CurrentActivity = this;
            if (string.IsNullOrEmpty(ActivityKey))
            {
                ActivityKey = NextPageKey;
                NextPageKey = null;
            }
            base.OnResume();
        }
    }
}
