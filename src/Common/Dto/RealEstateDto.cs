using System;

namespace Common.DTO
{
    public record RealEstateDto
    {
        public RealEstateDto(string street, 
            string city, 
            int zipCode, 
            string state,
            int beds,
            int baths,
            int sqFt,
            string type,
            DateTime saleDate,
            decimal price,
            float latitude,
            float longtitude)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            State = state;
            Beds = beds;
            Baths = baths;
            SqFt = sqFt;
            Type = type;
            SaleDate = saleDate;
            Price = price;
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public string Street { get; }
        public string City { get; }
        public int ZipCode { get; }
        public string State { get; }
        public int Beds { get; }
        public int Baths { get; }
        public int SqFt { get; }
        public string Type { get; }
        public DateTime SaleDate { get; }
        public decimal Price { get; }
        public float Latitude { get; }
        public float Longtitude { get; }
    }
}
