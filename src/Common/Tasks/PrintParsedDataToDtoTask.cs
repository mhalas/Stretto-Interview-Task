using Common.Common;
using Common.DTO;
using System.Collections.Generic;

namespace Common.Tasks
{
    public class PrintParsedDataToDtoTask: IStrettoTask
    {
        private readonly ResultListFormater _resultListFormater;
        private readonly IEnumerable<RealEstateDto> _realEstateList;

        public PrintParsedDataToDtoTask(ResultListFormater resultListFormater, IEnumerable<RealEstateDto> realEstateList)
        {
            _resultListFormater = resultListFormater;
            _realEstateList = realEstateList;
        }

        public string Execute()
        {
            return _resultListFormater.PrintResult(_realEstateList);
        }
    }
}
