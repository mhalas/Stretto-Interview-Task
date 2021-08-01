using Common.Common;
using Common.DTO;
using Common.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Tasks
{
    public class PrintParsedDataToDtoTaskTest
    {
        [Test]
        public void Task_PrintParsedDataToDto()
        {
            List<RealEstateDto> list = new List<RealEstateDto>()
            {
                new RealEstateDto("a1","a2",13,"a4",15,16,1,"a8", new DateTime(2020,01,09), 110, 111, 112),
                new RealEstateDto("b1","b2",23,"b4",25,26,2,"b8", new DateTime(2020,02,09), 210, 211, 212),
                new RealEstateDto("c1","c2",33,"c4",35,36,3,"c8", new DateTime(2020,03,09), 310, 311, 312),
                new RealEstateDto("d1","d2",43,"d4",45,46,4,"d8", new DateTime(2020,04,09), 410, 411, 412),
            };

            ResultListFormater resultListFormater = new ResultListFormater();

            PrintParsedDataToDtoTask task = new PrintParsedDataToDtoTask(resultListFormater, list);
            var result = task.Execute();

            var expectedResult = "1. RealEstateDto { Street = a1, City = a2, Zip = 13, State = a4, Beds = 15, Baths = 16, SqFt = 1, Type = a8, SaleDate = 09.01.2020 00:00:00, Price = 110, Latitude = 111, Longitude = 112 }\r\n" +
                "2. RealEstateDto { Street = b1, City = b2, Zip = 23, State = b4, Beds = 25, Baths = 26, SqFt = 2, Type = b8, SaleDate = 09.02.2020 00:00:00, Price = 210, Latitude = 211, Longitude = 212 }\r\n" +
                "3. RealEstateDto { Street = c1, City = c2, Zip = 33, State = c4, Beds = 35, Baths = 36, SqFt = 3, Type = c8, SaleDate = 09.03.2020 00:00:00, Price = 310, Latitude = 311, Longitude = 312 }\r\n" +
                "4. RealEstateDto { Street = d1, City = d2, Zip = 43, State = d4, Beds = 45, Baths = 46, SqFt = 4, Type = d8, SaleDate = 09.04.2020 00:00:00, Price = 410, Latitude = 411, Longitude = 412 }";

            Assert.AreEqual(expectedResult, result);
        }
    }
}
