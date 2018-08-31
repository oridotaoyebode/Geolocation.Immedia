using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Immedia.Demo.Geolocation.Model;

namespace Immedia.Demo.Geolocation.Services
{
    public interface IGooglePlaceService
    {
        Task<Place> GetNearbyPlaces(GeoLocation geoLocation);
        Task<PlaceDetails> GetPlaceDetails(string placeid);
    }
}