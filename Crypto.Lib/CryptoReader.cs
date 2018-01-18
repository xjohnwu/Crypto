using System;
using System.Collections.Generic;
using Crypto.Lib.Json;
using Crypto.Lib.Json.Converters;
using Crypto.Lib.Model;
using Newtonsoft.Json;
using RestSharp;

namespace Crypto.Lib
{
    public class CryptoReader
    {
        private readonly IRestClient _client;

        public CryptoReader()
            : this(GetClient())
        {
        }

        public CryptoReader(IRestClient client)
        {
            _client = client;
        }

        private static IRestClient GetClient()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://min-api.cryptocompare.com/data/")
            };
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NAConverter());
            var serializer = new CustomJsonSerializer(settings);
            client.AddHandler(serializer.ContentType, serializer);
            return client;
        }

        private static IRestRequest BuildRequest(string resource)
        {
            return new RestRequest(Method.GET)
            {
                Resource = resource,
                RequestFormat = DataFormat.Json,
            };
        }

        private T GetResponse<T>(IRestRequest request) where T : new()
        {
            var restResponse = _client.Execute<T>(request);
            return restResponse.Data;
        }

        public IEnumerable<Coin> GetCoinList()
        {
            var request = BuildRequest("all/coinlist");
            var response = GetResponse<CoinList>(request);
            return response.Data.Values;
        }

        public IEnumerable<DataPoint> GetHistoricalData(string baseCcy, string termCcy, DateTime from, DateTime to)
        {
            from = DateHelper.HandleDate(from);
            to = DateHelper.HandleDate(to);
            var request = BuildRequest("histoday");
            request.AddParameter("fsym", baseCcy);
            request.AddParameter("tsym", termCcy);
            request.AddParameter("aggregate", 1);
            request.AddParameter("toTs", DateHelper.ToEpochTime(to));
            request.AddParameter("limit", (long)(to-from).TotalDays);

            var response = GetResponse<HistoricalData>(request);
            return response.Data;
        }
    }
}
