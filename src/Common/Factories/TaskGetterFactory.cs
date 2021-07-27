using Common.Common;
using Common.DTO;
using Common.Enums;
using Common.Tasks;
using System.Collections.Generic;

namespace Common.Factories
{
    public class TaskGetterFactory
    {
        private readonly ResultListFormater _resultListFormater;
        private readonly string _input;
        private readonly IEnumerable<RealEstateDto> _realEstateList;
        private readonly IDictionary<string, decimal> _taxList;

        public TaskGetterFactory(ResultListFormater resultListFormater, 
            string input,
            IEnumerable<RealEstateDto> realEstateList,
            IDictionary<string, decimal> taxList)
        {
            _resultListFormater = resultListFormater;
            _input = input;
            _realEstateList = realEstateList;
            _taxList = taxList;
        }

        public IStrettoTask GetTask(StrettoTaskType type)
        {
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
                    return new FindMostExpensiveApartmentForCityTask(_resultListFormater, _realEstateList, _taxList);
                default:
                    return null;
            }
        }
    }
}
