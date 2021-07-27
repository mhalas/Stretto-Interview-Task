using Common.Common;
using Common.DTO;
using Common.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Tasks
{
    public class FindMostExpensiveApartmentForCityTaskTest
    {
        [Test]
        public void Task_FindMostExpensiveApartmentForCity()
        {
            List<RealEstateDto> list = new List<RealEstateDto>()
            {
                new RealEstateDto("A","City1",0,"",0,0,0,"", new DateTime(2020,01,01), 100, 0, 0),

                new RealEstateDto("B","City2",0,"",0,0,0,"", new DateTime(2020,01,01), 100, 0, 0),
                new RealEstateDto("C","City2",0,"",0,0,0,"", new DateTime(2020,01,01), 110, 0, 0),
                new RealEstateDto("D","City2",0,"",0,0,0,"", new DateTime(2020,01,01), 120, 0, 0),

                new RealEstateDto("E","City3",0,"",0,0,0,"", new DateTime(2020,01,01), 210, 0, 0),
                new RealEstateDto("G","City3",0,"",0,0,0,"", new DateTime(2020,01,01), 230, 0, 0),
                new RealEstateDto("F","City3",0,"",0,0,0,"", new DateTime(2020,01,01), 220, 0, 0),
            };

            var taxesInCities = new Dictionary<string, decimal>()
            {
                { "City1", 0.5m },
                { "City2", 0.1m },
                { "City3", 0.75m },
            };

            ResultListFormater resultListFormater = new ResultListFormater();

            FindMostExpensiveApartmentForCityTask task = new FindMostExpensiveApartmentForCityTask(resultListFormater, list, taxesInCities);
            var result = task.Execute();

            var expectedResult = "1. RealEstateDto { Street = A, City = City1, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 100, Latitude = 0, Longtitude = 0 }\r\n" +
                "2. RealEstateDto { Street = D, City = City2, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 120, Latitude = 0, Longtitude = 0 }\r\n" +
                "3. RealEstateDto { Street = G, City = City3, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 230, Latitude = 0, Longtitude = 0 }";

            Assert.AreEqual(expectedResult, result);
        }
    }
}
