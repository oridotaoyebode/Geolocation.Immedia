using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Immedia.Demo.Geolocation.Helpers
{
    public static class DeserializationService
    {
        public static T Deserialize<T>(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            return default(T);
        }

        public static string Serialize<T>(T content)
        {                                                //
            string s = JsonConvert.SerializeObject(content, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return s;
            // Debug.WriteLine(s);
        }
    }
}