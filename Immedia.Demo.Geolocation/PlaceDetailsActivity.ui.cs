using Android.Widget;
using GalaSoft.MvvmLight.Views;
using Immedia.Demo.Geolocation.Helpers;

namespace Immedia.Demo.Geolocation
{
    public partial class PlaceDetailsActivity : BaseActivity
    {
        private GridView _gridView;

        public GridView GridView
        {
            get
            {
                return _gridView
                       ?? (_gridView = FindViewById<GridView>(Resource.Id.gridview));
            }
        }
    }
}