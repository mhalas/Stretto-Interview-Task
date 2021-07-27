using Common.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Common.Converters
{
    public class ConvertCsvToDtoList
    {
        public IEnumerable<RealEstateDto> Convert(string data, string dateTimeFormat)
        {
            var rows = data
                .Split(Environment.NewLine, StringSplitOptions.None)
                .Select(x => x.Split(','))
                .ToList();

            var headerRow = rows.First();

            rows.RemoveAt(0);

            return rows.Select(x => new RealEstateDto(x[0],
                x[1],
                int.Parse(x[2]),
                x[3],
                int.Parse(x[4]),
                int.Parse(x[5]),
                int.Parse(x[6]),
                x[7],
                DateTime.ParseExact(x[8], dateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(x[9]),
                float.Parse(x[10], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(x[11], CultureInfo.InvariantCulture.NumberFormat)));
        }
    }
}
