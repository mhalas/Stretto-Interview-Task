using Common.Common;
using Common.DTO;
using Common.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Tasks
{
    public class FindLargestApartmentForCityFilteredByTypeTaskTest
    {
        [TestCase("Type1", "No data")]
        [TestCase("Type2", "1. RealEstateDto { Street = A, City = , ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 1, Type = Type2, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }")]
        [TestCase("Type3", "1. RealEstateDto { Street = D, City = , ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 3, Type = Type3, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }")]
        [TestCase("Type4", "1. RealEstateDto { Street = G, City = , ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 8, Type = Type4, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }")]
        public void Task_FindLargestApartmentForCityFilteredByType_ReturnOnlyOneElementForType(string typeFilter, string expectedResult)
        {
            List<RealEstateDto> list = new List<RealEstateDto>()
            {
                new RealEstateDto("A","",0,"",0,0,1,"Type2", new DateTime(2020,01,01), 0, 0, 0),

                new RealEstateDto("B","",0,"",0,0,1,"Type3", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("C","",0,"",0,0,2,"Type3", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("D","",0,"",0,0,3,"Type3", new DateTime(2020,01,01), 0, 0, 0),

                new RealEstateDto("E","",0,"",0,0,6,"Type4", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("G","",0,"",0,0,8,"Type4", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("F","",0,"",0,0,7,"Type4", new DateTime(2020,01,01), 0, 0, 0),
            };

            ResultListFormater resultListFormater = new ResultListFormater();

            FindLargestApartmentForCityFilteredByTypeTask task = new FindLargestApartmentForCityFilteredByTypeTask(resultListFormater, list, typeFilter);
            var result = task.Execute();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Task_FindLargestApartmentForCityFilteredByType_ReturnAllRowsForConcreteCity()
        {
            var typeToTest = "Type1";

            List<RealEstateDto> list = new List<RealEstateDto>()
            {
                new RealEstateDto("A","City1",0,"",0,0,1,"Type1", new DateTime(2020,01,01), 0, 0, 0),

                new RealEstateDto("B","City2",0,"",0,0,1,"Type1", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("C","City3",0,"",0,0,2,"Type1", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("D","City3",0,"",0,0,3,"Type2", new DateTime(2020,01,01), 0, 0, 0),

                new RealEstateDto("E","City4",0,"",0,0,6,"Type1", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("G","City4",0,"",0,0,8,"Type1", new DateTime(2020,01,01), 0, 0, 0),
                new RealEstateDto("F","City4",0,"",0,0,7,"Type1", new DateTime(2020,01,01), 0, 0, 0),
            };

            ResultListFormater resultListFormater = new ResultListFormater();

            FindLargestApartmentForCityFilteredByTypeTask task = new FindLargestApartmentForCityFilteredByTypeTask(resultListFormater, list, typeToTest);
            var result = task.Execute();

            var expectedResult = "1. RealEstateDto { Street = A, City = City1, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 1, Type = Type1, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }\r\n" +
                "2. RealEstateDto { Street = B, City = City2, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 1, Type = Type1, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }\r\n" +
                "3. RealEstateDto { Street = C, City = City3, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 2, Type = Type1, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }\r\n" +
                "4. RealEstateDto { Street = G, City = City4, ZipCode = 0, State = , Beds = 0, Baths = 0, SqFt = 8, Type = Type1, SaleDate = 01.01.2020 00:00:00, Price = 0, Latitude = 0, Longtitude = 0 }";

            Assert.AreEqual(expectedResult, result);
        }
    }
}
