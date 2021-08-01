using System;

namespace Common.DTO
{
    public record RealEstateDto
    {
        public RealEstateDto()
        {

        }

        public RealEstateDto(string street, 
            string city, 
            int zip, 
            string state,
            int beds,
            int baths,
            int sqFt,
            string type,
            DateTime saleDate,
            decimal price,
            float latitude,
            float longitude)
        {
            Street = street;
            City = city;
            Zip = zip;
            State = state;
            Beds = beds;
            Baths = baths;
            SqFt = sqFt;
            Type = type;
            SaleDate = saleDate;
            Price = price;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string State { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SqFt { get; set; }
        public string Type { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Price { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
