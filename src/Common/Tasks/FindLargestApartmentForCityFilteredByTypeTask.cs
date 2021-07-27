using Common.Common;
using Common.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Common.Tasks
{
    public class FindLargestApartmentForCityFilteredByTypeTask : IStrettoTask
    {
        private readonly ResultListFormater _resultListFormater;
        private readonly IEnumerable<RealEstateDto> _realEstateList;
        private readonly string _typeFilter;

        public FindLargestApartmentForCityFilteredByTypeTask(ResultListFormater resultListFormater, IEnumerable<RealEstateDto> realEstateList, string typeFilter)
        {
            _resultListFormater = resultListFormater;
            _realEstateList = realEstateList;
            _typeFilter = typeFilter;
        }

        public string Execute()
        {
            var resultList = _realEstateList
                .GroupBy(x => x.City)
                .Select(x => x.Where(y => y.Type == _typeFilter)
                .OrderByDescending(y => y.SqFt )
                .FirstOrDefault());

            return _resultListFormater.PrintResult(resultList);
        }
    }
}
