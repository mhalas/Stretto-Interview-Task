using Common.DTO;
using CsvHelper.Configuration;

namespace Common.Dto
{
    public class RealEstateMapper: ClassMap<RealEstateDto>
    {
        public RealEstateMapper(string dateTimeFormat)
        {
            Map(x => x.Baths)
                .Name("baths");

            Map(x => x.Beds)
                .Name("beds");

            Map(x => x.City)
                .Name("city");

            Map(x => x.Latitude)
                .Name("latitude");

            Map(x => x.Longitude)
                .Name("longitude");

            Map(x => x.Price)
                .Name("price");

            Map(x => x.SaleDate)
                .Name("sale_date")
                .TypeConverterOption
                .Format(dateTimeFormat);


            Map(x => x.SqFt)
                .Name("sq__ft");

            Map(x => x.State)
                .Name("state");

            Map(x => x.Street)
                .Name("street");

            Map(x => x.Type)
                .Name("type");

            Map(x => x.Zip)
                .Name("zip");
        }
    }
}
