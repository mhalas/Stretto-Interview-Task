using Common.Common;
using Common.DTO;
using Common.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Tasks
{
    public class FindCheapestApartmentWithLargestNumberOfRoomsTaskTest
    {
        [TestCase(1, 1, 100, 1, 2, 100, 2, 3, 100, "1. RealEstateDto { Street = C, City = , ZipCode = 0, State = , Beds = 2, Baths = 3, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 100, Latitude = 0, Longtitude = 0 }")]
        [TestCase(1, 1, 100, 5, 2, 100, 1, 1, 100, "1. RealEstateDto { Street = B, City = , ZipCode = 0, State = , Beds = 5, Baths = 2, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 100, Latitude = 0, Longtitude = 0 }")]
        [TestCase(1, 1, 100, 3, 2, 100, 2, 3, 100, "1. RealEstateDto { Street = B, City = , ZipCode = 0, State = , Beds = 3, Baths = 2, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 100, Latitude = 0, Longtitude = 0 }\r\n2. RealEstateDto { Street = C, City = , ZipCode = 0, State = , Beds = 2, Baths = 3, SqFt = 0, Type = , SaleDate = 01.01.2020 00:00:00, Price = 100, Latitude = 0, Longtitude = 0 }")]
        public void Task_FindCheapestApartmentWithLargestNumberOfRooms(
            int firstBeds, int firstBaths, decimal firstPrice, 
            int secondBeds, int secondBaths, decimal secondPrice, 
            int thirdBeds, int thirdBaths, decimal thirdPrice,
            string expectedResult)
        {
            List<RealEstateDto> list = new List<RealEstateDto>()
            {
                new RealEstateDto("A","",0,"",firstBeds,firstBaths,0,"", new DateTime(2020,01,01), firstPrice, 0, 0),
                new RealEstateDto("B","",0,"",secondBeds,secondBaths,0,"", new DateTime(2020,01,01), secondPrice, 0, 0),
                new RealEstateDto("C","",0,"",thirdBeds,thirdBaths,0,"", new DateTime(2020,01,01), thirdPrice, 0, 0),
            };

            ResultListFormater resultListFormater = new ResultListFormater();

            FindCheapestApartmentWithLargestNumberOfRoomsTask task = new FindCheapestApartmentWithLargestNumberOfRoomsTask(resultListFormater, list);
            var result = task.Execute();

            Assert.AreEqual(expectedResult, result);
        }
    }
}
