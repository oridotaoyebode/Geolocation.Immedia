using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Immedia.Demo.Geolocation.Constants;
using Immedia.Demo.Geolocation.Helpers;
using Immedia.Demo.Geolocation.Model;

namespace Immedia.Demo.Geolocation.Services
{
    public class GooglePlaceService: IGooglePlaceService
    {
        public async Task<Place> GetNearbyPlaces(GeoLocation geoLocation)
        {
            if (geoLocation.Latitude == null || geoLocation.Longitude == null) return default(Place);
            var googlePlaceSearchUrl =
                $"place/nearbysearch/json?location={geoLocation.Latitude.Value.ToString(CultureInfo.InvariantCulture)},{geoLocation.Longitude.Value.ToString(CultureInfo.InvariantCulture)}&radius=1000&key={FileConstants.GooglePlacesApiKey}";
            var apiJsonValue =
                await HttpService.GetAsync(FileConstants.GooglePlacesApiBaseUrl, googlePlaceSearchUrl);
            if (apiJsonValue != null)
            {
                return Place.FromJson(apiJsonValue);
            }

            return default(Place);
        }

        public async Task<PlaceDetails> GetPlaceDetails(string placeid)
        {
            var googlePlaceDetailsSearchUrl =
                $"place/details/json?placeid={placeid}&key={FileConstants.GooglePlacesApiKey}";
            var apiJsonValue =
                await HttpService.GetAsync(FileConstants.GooglePlacesApiBaseUrl, googlePlaceDetailsSearchUrl);
            return apiJsonValue != null ? PlaceDetails.FromJson(apiJsonValue) : default(PlaceDetails);
        }
    }
}