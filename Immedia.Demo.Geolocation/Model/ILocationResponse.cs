using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Immedia.Demo.Geolocation.Constants;

namespace Immedia.Demo.Geolocation.Model
{
    public interface ILocationResponse
    {
    }

    public class GeoLocation : ILocationResponse
    {
        public GeoLocation()
        {
            
        }
        public GeoLocation(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public static GeoLocation Parse(string location)
        {
            GeoLocation result = new GeoLocation();

            try
            {
                var locationSetting = FileConstants.DefaultFallbackMapsLocation;
                var locationParts = locationSetting.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                result.Latitude = double.Parse(locationParts[0], CultureInfo.InvariantCulture);
                result.Longitude = double.Parse(locationParts[1], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing location: {ex}");
            }

            return result;
        }
    }
}