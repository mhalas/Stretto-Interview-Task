using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ApiClient
{
    public class StrettoApi
    {
        private readonly string _apiAddress;
        private readonly string _csvEndpoint;
        private readonly string _taxesEndpoint;

        public StrettoApi(string apiAddress, string csvEndpoint, string taxesEndpoint)
        {
            _apiAddress = apiAddress;
            _csvEndpoint = csvEndpoint;
            _taxesEndpoint = taxesEndpoint;
        }

        public string GetCsvData()
        {
            var clientApi = new RestClient(_apiAddress);
            var restRequest = new RestRequest(_csvEndpoint);

            var result = clientApi.DownloadData(restRequest);

            if (result == null || !result.Any())
                return string.Empty;

            return Encoding.UTF8.GetString(result);
        }

        public IDictionary<string, decimal> GetTaxesForCities(IEnumerable<string> cityList)
        {
            var result = new Dictionary<string, decimal>();

            var clientApi = new RestClient(_apiAddress);

            foreach (var city in cityList)
            {
                var restRequest = new RestRequest(string.Format(_taxesEndpoint, city));
                var restResponse = clientApi.Execute<decimal>(restRequest);

                if (!restResponse.IsSuccessful)
                    throw new Exception(restResponse.ErrorMessage);

                result.Add(city, restResponse.Data);
            }

            return result;
        }
    }
}
