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
using Newtonsoft.Json;

namespace Immedia.Demo.Geolocation.Model
{
    public class PlaceDetails
    {
        [JsonProperty("html_attributions")]
        public object[] HtmlAttributions { get; set; }

        [JsonProperty("result")]
        public PlacesSearchResult.Result Result { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public static PlaceDetails FromJson(string json) => JsonConvert.DeserializeObject<PlaceDetails>(json, PlacesSearchResult.Converter.Settings);
    }
}