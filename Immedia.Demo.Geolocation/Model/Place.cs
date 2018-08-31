using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Immedia.Demo.Geolocation.Model
{
    public  partial class Place
    {
        [JsonProperty("html_attributions")]
        public object[] HtmlAttributions { get; set; }

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public PlacesSearchResult.Result[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public static Place FromJson(string json) => JsonConvert.DeserializeObject<Place>(json,  PlacesSearchResult.Converter.Settings);
        
    }
}
