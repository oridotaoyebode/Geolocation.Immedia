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
using Immedia.Demo.Geolocation.Model;
using Object = Java.Lang.Object;

namespace Immedia.Demo.Geolocation.Helpers
{
    public class GridViewAdapter: BaseAdapter<PlacesSearchResult.Photo>
    {
        Context context;
        public GridViewAdapter(Context c, List<PlacesSearchResult.Photo> photos)
        {
            context = c;
            Photos = photos;
        }
        
        public override PlacesSearchResult.Photo this[int position] => this.Photos[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {           
            ImageViewAsync imageView;
            if (convertView == null)
            {  // if it's not recycled, initialize some attributes
                imageView = new ImageViewAsync(context) {LayoutParameters = new AbsListView.LayoutParams(150, 150)};
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(2, 2, 2, 2);
            }
            else
            {
                imageView = (ImageViewAsync)convertView;
            }
            ImageService.Instance.LoadUrl(this.Photos[position].ImageUrl).Into(imageView);
            return imageView;
        }

        public override int Count => Photos.Count;

        public List<PlacesSearchResult.Photo> Photos { get; set; }
    }
}