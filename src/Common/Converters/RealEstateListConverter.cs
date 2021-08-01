using Common.Dto;
using Common.DTO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Common.Converters
{
    public class RealEstateListConverter : IRealEstateListConverter
    {
        public IEnumerable<RealEstateDto> Convert(string data, string dateTimeFormat)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };

            using (var reader = new StringReader(data))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                var realEstateMapper = new RealEstateMapper(dateTimeFormat);

                csv.Context.RegisterClassMap(realEstateMapper);

                return csv.GetRecords<RealEstateDto>().ToList();
            }
        }
    }
}
