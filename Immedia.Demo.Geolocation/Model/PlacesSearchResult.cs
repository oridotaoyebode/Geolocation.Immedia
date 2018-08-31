using System;
using System.Collections.Generic;
using System.Globalization;
using GalaSoft.MvvmLight;
using Immedia.Demo.Geolocation.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Exception = Java.Lang.Exception;

namespace Immedia.Demo.Geolocation.Model
{
    public class PlacesSearchResult
    {

        public class Geometry
        {
            [JsonProperty("location")]
            public Location Location { get; set; }

            [JsonProperty("viewport")]
            public Viewport Viewport { get; set; }
        }

        public class Location
        {
            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lng")]
            public double Lng { get; set; }
        }

        public class Viewport
        {
            [JsonProperty("northeast")]
            public Location Northeast { get; set; }

            [JsonProperty("southwest")]
            public Location Southwest { get; set; }
        }

        public class OpeningHours
        {
            [JsonProperty("open_now")]
            public bool OpenNow { get; set; }
        }

        public class Photo
        {
            [JsonProperty("height")]
            public long Height { get; set; }

            [JsonProperty("html_attributions")]
            public string[] HtmlAttributions { get; set; }

            [JsonProperty("photo_reference")]
            public string PhotoReference { get; set; }

            [JsonProperty("width")]
            public long Width { get; set; }

            public string ImageUrl =>
                $"{FileConstants.GooglePlacesApiBaseUrl}place/photo?maxwidth=400&photoreference={PhotoReference}&key={Constants.FileConstants.GooglePlacesApiKey}";
        }

        public class PlusCode
        {
            [JsonProperty("compound_code")]
            public string CompoundCode { get; set; }

            [JsonProperty("global_code")]
            public string GlobalCode { get; set; }
        }

        public enum Scope { Google };

        



        public static class Serialize
        {
            public static string ToJson(Place self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters = {
                ScopeConverter.Singleton,
               
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class ScopeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Scope) || t == typeof(Scope?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                if (value == "GOOGLE")
                {
                    return Scope.Google;
                }
                throw new Exception("Cannot unmarshal type Scope");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Scope)untypedValue;
                if (value == Scope.Google)
                {
                    serializer.Serialize(writer, "GOOGLE");
                    return;
                }
                throw new Exception("Cannot marshal type Scope");
            }

            public static readonly ScopeConverter Singleton = new ScopeConverter();
        }

       

        public class Result: ObservableObject
        {
            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("photos")]
            public List<Photo> Photos { get; set; }

            [JsonProperty("place_id")]
            public string PlaceId { get; set; }

            [JsonProperty("reference")]
            public string Reference { get; set; }

            [JsonProperty("scope")]
            public Scope Scope { get; set; }

            

            [JsonProperty("vicinity")]
            public string Vicinity { get; set; }

            [JsonProperty("opening_hours", NullValueHandling = NullValueHandling.Ignore)]
            public OpeningHours OpeningHours { get; set; }

            [JsonProperty("plus_code", NullValueHandling = NullValueHandling.Ignore)]
            public PlusCode PlusCode { get; set; }

            [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
            public double? Rating { get; set; }

            [JsonProperty("price_level", NullValueHandling = NullValueHandling.Ignore)]
            public long? PriceLevel { get; set; }
        }
    }
}
