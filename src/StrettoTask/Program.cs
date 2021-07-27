using Common.ApiClient;
using Common.Common;
using Common.Converters;
using Common.Enums;
using Common.Factories;
using System;
using System.Linq;

namespace StrettoTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stretto interview task.");
            Console.WriteLine("Loading...");

            var apiAddress = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/";
            var csvEndpoint = "csv";
            var taxesEndpoint = "taxes?city={0}";
            var dateTimeFormat = "ddd MMM dd hh:mm:ss EDT yyyy";

            var resultListFormater = new ResultListFormater();
            var convertCsvToDtoList = new ConvertCsvToDtoList();

            var strettoApi = new StrettoApi(apiAddress, csvEndpoint, taxesEndpoint);

            Console.WriteLine("Downloading csv...");
            var csvPlainText = strettoApi.GetCsvData();
            Console.WriteLine("Success.");

            var realEstateList = convertCsvToDtoList.Convert(csvPlainText, dateTimeFormat);

            var cityList = realEstateList.Select(x => x.City).Distinct();

            Console.WriteLine("Download taxes.");
            var taxList = strettoApi.GetTaxesForCities(cityList);
            Console.WriteLine("Success.");

            var taskFactory = new TaskGetterFactory(resultListFormater, csvPlainText, realEstateList, taxList);

            Console.WriteLine("Loading complete. Press any key to continue...");
            Console.ReadKey();

            while(true)
            {
                Console.Clear();
                Console.WriteLine("Menu:\r\n" +
                    "Choose option number." +
                    "1. Print data from plaintext.\r\n" +
                    "2. Print parsed data to dto list type.\r\n" +
                    "3. Find largest apartment for city, filtered by 'Residential' type.\r\n" +
                    "4. Find cheapest apartment with largest number of rooms.\r\n" +
                    "5. Find most expensive apartment for every city.\r\n" +
                    "6. Exit.");
                Console.Write("Answer: ");
                var answer = Console.ReadLine();

                var answerInt = 0;

                if(!int.TryParse(answer, out answerInt))
                {
                    Console.WriteLine("Cant parse answer. Please try again. Press any key to continue...");
                    Console.ReadKey();

                    continue;
                }

                if (answerInt == 6)
                    break;

                var task = taskFactory.GetTask((StrettoTaskType)answerInt);
                Console.WriteLine("Print response:");
                var response = task.Execute();
                Console.WriteLine(response);
                Console.WriteLine("Press any key to back to menu...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("Closing application. Press any key to close application.");
        }
    }
}
