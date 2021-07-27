using Common.Common;
using Common.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Common.Tasks
{
    public class FindMostExpensiveApartmentForCityTask : IStrettoTask
    {
        private readonly ResultListFormater _resultListFormater;
        private readonly IEnumerable<RealEstateDto> _realEstateList;
        private readonly IDictionary<string, decimal> _taxesInCities;

        public FindMostExpensiveApartmentForCityTask(ResultListFormater resultListFormater, IEnumerable<RealEstateDto> realEstateList, IDictionary<string, decimal> taxesInCities)
        {
            _resultListFormater = resultListFormater;
            _realEstateList = realEstateList;
            _taxesInCities = taxesInCities;
        }

        public string Execute()
        {
            var resultList = _realEstateList
                .GroupBy(x => x.City)
                .Select(x => x.OrderByDescending(x=> (x.Price + x.Price * _taxesInCities.First(y=>y.Key == x.City).Value)).First());

            return _resultListFormater.PrintResult(resultList);
        }
    }
}
