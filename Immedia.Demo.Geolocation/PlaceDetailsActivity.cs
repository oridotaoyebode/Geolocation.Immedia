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
using GalaSoft.MvvmLight.Views;
using Immedia.Demo.Geolocation.Helpers;
using Immedia.Demo.Geolocation.Model;
using Immedia.Demo.Geolocation.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Immedia.Demo.Geolocation
{
    [Activity(Label = "Details", Theme = "@style/MyTheme")]
    public partial class PlaceDetailsActivity
    {
        private PlaceDetails Vm { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Details);

            Vm = ((BaseNavigationService) ServiceLocator.Current.GetInstance<INavigationService>())
                .GetAndRemoveParameter<PlaceDetails>(Intent);
            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            gridview.Adapter = new GridViewAdapter(this, Vm.Result.Photos);

            gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                var intent = new Intent(this, typeof(FullScreenActivity));
                intent.PutExtra("photo", Vm.Result.Photos[args.Position].ImageUrl);
                StartActivity(intent);
            };
        }
    }
}