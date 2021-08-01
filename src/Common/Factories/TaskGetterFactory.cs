using Common.ApiClient;
using Common.Common;
using Common.Converters;
using Common.Dto;
using Common.DTO;
using Common.Enums;
using Common.Tasks;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Factories
{
    public class TaskGetterFactory
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly AppConfigurationDto _configuration;
        private readonly ResultListFormater _resultListFormater;
        private readonly StrettoApiClientDecorator _apiClient;
        private readonly IRealEstateListConverter _realEstateListConverter;
        private readonly string _input;

        private IEnumerable<RealEstateDto> _realEstateList;
        private IDictionary<string, decimal> _taxList;

        public TaskGetterFactory(AppConfigurationDto configuration,
            ResultListFormater resultListFormater,
            StrettoApiClientDecorator ApiClient,
            IRealEstateListConverter realEstateListConverter,
            string input)
        {
            _configuration = configuration;
            _resultListFormater = resultListFormater;
            _apiClient = ApiClient;
            _realEstateListConverter = realEstateListConverter;
            _input = input;
        }

        public async Task<IStrettoTask> GetTask(StrettoTaskType type)
        {
            _logger.Info("Getting task '{0}'.", type.ToString());

            if(type != StrettoTaskType.PrintCsv
                && _realEstateList == null)
            {
                _realEstateList = _realEstateListConverter.Convert(_input, _configuration.DateTimeFormat);
            }

            switch (type)
            {
                case StrettoTaskType.PrintCsv:
                    return new PrintCSVTask(_input);
                case StrettoTaskType.PrintParsedDataToDto:
                    return new PrintParsedDataToDtoTask(_resultListFormater, _realEstateList);
                case StrettoTaskType.FindLargestApartmentForCityFilteredByType:
                    return new FindLargestApartmentForCityFilteredByTypeTask(_resultListFormater, _realEstateList, "Residential");
                case StrettoTaskType.FindCheapestApartmentWithLargestNumberOfRooms:
                    return new FindCheapestApartmentWithLargestNumberOfRoomsTask(_resultListFormater, _realEstateList);
                case StrettoTaskType.FindMostExpensiveApartmentForCity:
                    if(_taxList == null)
                    {
                        var cityList = _realEstateList.Select(x => x.City).Distinct();
                        _taxList = await _apiClient.GetTaxesForCities(cityList);
                    }

                    return new FindMostExpensiveApartmentForCityTask(_resultListFormater, _realEstateList, _taxList);
                default:
                    return null;
            }
        }
    }
}
