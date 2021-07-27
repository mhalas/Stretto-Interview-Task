using Common.Common;
using Common.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Common.Tasks
{
    public class FindCheapestApartmentWithLargestNumberOfRoomsTask: IStrettoTask
    {
        private readonly ResultListFormater _resultListFormater;
        private readonly IEnumerable<RealEstateDto> _realEstateList;

        public FindCheapestApartmentWithLargestNumberOfRoomsTask(ResultListFormater resultListFormater, IEnumerable<RealEstateDto> realEstateList)
        {
            _resultListFormater = resultListFormater;
            _realEstateList = realEstateList;
        }

        public string Execute()
        {
            var result = _realEstateList
                .GroupBy(x => (x.Baths + x.Beds) / x.Price)
                .OrderByDescending(x => x.Key)
                .First()
                .ToList();

            return _resultListFormater.PrintResult(result);
        }
    }
}
