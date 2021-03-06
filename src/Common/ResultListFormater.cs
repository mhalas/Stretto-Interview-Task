using Common.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Common
{
    public class ResultListFormater
    {
        public string PrintResult(RealEstateDto realEstateDto, int rowNumber = 1)
        {
            return string.Format("{0}. {1}", rowNumber, realEstateDto.ToString());
        }

        public string PrintResult(IEnumerable<RealEstateDto> realEstateList)
        {
            if (realEstateList == null || !realEstateList.Any())
            {
                return "No data";
            }

            StringBuilder result = new StringBuilder();

            var rowNumber = 1;

            foreach (var item in realEstateList)
            {
                if (item == null)
                {
                    continue;
                }

                if (result.Length != 0)
                {
                    result.AppendLine();
                }

                result.Append(PrintResult(item, rowNumber));

                rowNumber++;
            }

            if (result.Length == 0)
            {
                return "No data";
            }

            return result.ToString();
        }
    }
}
