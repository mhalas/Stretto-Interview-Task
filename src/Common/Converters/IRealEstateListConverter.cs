using Common.DTO;
using System.Collections.Generic;

namespace Common.Converters
{
    public interface IRealEstateListConverter
    {
        IEnumerable<RealEstateDto> Convert(string data, string dateTimeFormat);
    }
}
