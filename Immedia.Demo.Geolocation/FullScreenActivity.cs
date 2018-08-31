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
using FFImageLoading;
using FFImageLoading.Views;
using GalaSoft.MvvmLight.Views;
using Immedia.Demo.Geolocation.Helpers;
using Immedia.Demo.Geolocation.Model;
using Microsoft.Practices.ServiceLocation;

namespace Immedia.Demo.Geolocation
{
    public class FullScreenActivity: BaseActivity
    {
        private ImageViewAsync _imageViewAsync;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PhotoFullScreen);

            _imageViewAsync = FindViewById<ImageViewAsync>(Resource.Id.imageViewFullScren);
            var photoUrl = Intent.GetStringExtra("photo");
            ImageService.Instance.LoadUrl(photoUrl).Into(_imageViewAsync);
        }
    }
}