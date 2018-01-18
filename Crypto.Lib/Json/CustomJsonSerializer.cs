using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Crypto.Lib.Json
{
    public class CustomJsonSerializer : ISerializer, IDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public CustomJsonSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
            ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            return Serialize(obj, Formatting.None);
        }

        public string Serialize(object obj, Formatting formatting)
        {
            return JsonConvert.SerializeObject(obj, formatting, _settings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content, _settings);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}
