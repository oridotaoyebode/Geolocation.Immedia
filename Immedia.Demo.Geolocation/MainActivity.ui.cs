using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using Immedia.Demo.Geolocation.Helpers;

namespace Immedia.Demo.Geolocation
{
    // In this partial Activity, we provide access to the UI elements.
    // This file is partial to keep things cleaner in the MainActivity.cs
    // See http://blog.galasoft.ch/posts/2014/11/structuring-your-android-activities/
    public partial class MainActivity : BaseActivity
    {

        private SwipeRefreshLayout _refreshLayout;

        public SwipeRefreshLayout RefreshLayout
        {
            get
            {
                return _refreshLayout
                       ?? (_refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher));
            }
        }

        private RecyclerView _placesList;

        public RecyclerView PlacesList
        {
            get
            {
                return _placesList
                       ?? (_placesList = FindViewById<RecyclerView>(Resource.Id.PlacesList));
            }
        }

    }
}