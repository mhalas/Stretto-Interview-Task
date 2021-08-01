using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.ApiClient
{
    public class StrettoApiClientDecorator
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IStrettoApiClient _apiClient;

        public StrettoApiClientDecorator(IStrettoApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string> GetCsvData()
        {
            _logger.Trace("Getting Csv data.");

            try
            {
                var response = await _apiClient.GetCsvData();

                if (!response.ResponseMessage.IsSuccessStatusCode)
                {
                    _logger.Error("API error, can't get csv data. Status code: '{0}', message: '{1}'", response.ResponseMessage.StatusCode, response.ResponseMessage.Content.ToString());
                    return string.Empty;
                }

                return response.StringContent;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occured.");
                throw;
            }
            
        }

        public async Task<IDictionary<string, decimal>> GetTaxesForCities(IEnumerable<string> cityList)
        {
            _logger.Trace("Getting taxes for cities.");

            var result = new Dictionary<string, decimal>();
            
            try
            {
                foreach (var city in cityList)
                {
                    var response = await _apiClient.GetTaxForCity(city);

                    if (!response.ResponseMessage.IsSuccessStatusCode)
                        throw new Exception(response.ResponseMessage.Content.ToString());

                    result.Add(city, response.GetContent());
                }

                return result;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occured.");
                throw;
            }

        }
    }
}
