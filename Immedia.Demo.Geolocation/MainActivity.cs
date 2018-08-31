using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Immedia.Demo.Geolocation.Helpers;
using Immedia.Demo.Geolocation.ViewModel;
using Messenger = GalaSoft.MvvmLight.Messaging.Messenger;

namespace Immedia.Demo.Geolocation
{
    [Activity(Label = "Geolocation Places", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public partial class MainActivity
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();
        private ObservableRecyclerAdapter<PlaceDetailsViewModel, CachingViewHolder> _adapter;

        /// <summary>
        /// Gets a reference to the MainViewModel from the ViewModelLocator.
        /// </summary>
        private MainViewModel Vm
        {
            get
            {
                return App.Locator.Main;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _adapter = Vm.Places.GetRecyclerAdapter(
                BindViewHolder,
                Resource.Layout.PlaceTemplate,
                OnItemClick);

            _bindings.Add(this.SetBinding(
                () => Vm.Loading,
                () => RefreshLayout.Refreshing));

            PlacesList.SetLayoutManager(new LinearLayoutManager(this));
            PlacesList.SetAdapter(_adapter);
            RefreshLayout.SetColorSchemeColors(Color.Red, Color.Yellow, Color.Green);
            RefreshLayout.Refresh += RefreshLayoutOnRefresh;
        }

        private void RefreshLayoutOnRefresh(object sender, EventArgs eventArgs)
        {
            Vm.RefreshCommand.Execute(null);
        }

        public void OnItemClick(int oldPosition, View oldView, int position, View view)
        {
            Vm.ShowDetailsCommand.Execute(Vm.Places[position]);
        }
        private void BindViewHolder(CachingViewHolder holder, PlaceDetailsViewModel placeDetails, int position)
        {
            

            var title = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
            title.Text = placeDetails.Place.Name;
            title.SetTextColor(Color.Black);
            var desc = holder.FindCachedViewById<TextView>(Resource.Id.DescriptionTextView);
            desc.Text = placeDetails.Place.Vicinity;
            desc.SetTextColor(Color.Black);
        }



    }
}

