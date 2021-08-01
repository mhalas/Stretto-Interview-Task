using Common.ApiClient;
using Common.Common;
using Common.Converters;
using Common.Dto;
using Common.Enums;
using Common.Factories;
using Microsoft.Extensions.Configuration;
using NLog;
using RestEase;
using System;
using System.Threading.Tasks;

namespace StrettoTask
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<AppConfigurationDto>();

            logger.Info("Stretto interview task.");

            var resultListFormater = new ResultListFormater();
            IRealEstateListConverter realEstateListConverter = new RealEstateListConverter();


            IStrettoApiClient apiClient = RestClient.For<IStrettoApiClient>(configuration.ApiAddress);
            var apiClientDecorator = new StrettoApiClientDecorator(apiClient);

            string csvPlainText = string.Empty;
            try
            {
                logger.Info("Getting csv...");
                csvPlainText = await apiClientDecorator.GetCsvData();
                logger.Info("Success.");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error occured while getting csv data from API.");

            }

            var taskFactory = new TaskGetterFactory(configuration, 
                resultListFormater, 
                apiClientDecorator, 
                realEstateListConverter, 
                csvPlainText);


            logger.Info("Loading complete. Press any key to continue...");
            Console.ReadKey();

            while(true)
            {
                Console.Clear();
                logger.Debug("Menu:\r\n" +
                    "Choose option number." +
                    "1. Print data from plaintext.\r\n" +
                    "2. Print parsed data to dto list type.\r\n" +
                    "3. Find largest apartment for city, filtered by 'Residential' type.\r\n" +
                    "4. Find cheapest apartment with largest number of rooms.\r\n" +
                    "5. Find most expensive apartment for every city.\r\n" +
                    "6. Exit.");
                logger.Trace("Answer: ");
                var answer = Console.ReadLine();

                logger.Info("User answer: '{0}'.", answer);

                var answerInt = 0;

                if(!int.TryParse(answer, out answerInt))
                {
                    logger.Info("Cant parse answer. Please try again. Press any key to continue...");
                    Console.ReadKey();

                    continue;
                }

                if (answerInt == 6)
                    break;

                var task = await taskFactory.GetTask((StrettoTaskType)answerInt);
                logger.Info("Executing task.");
                var response = task.Execute();
                logger.Debug("Result: \r\n{0}.", response);
                logger.Info("Press any key to back to menu...");
                Console.ReadKey();
            }

            Console.Clear();
            logger.Info("Closing application. Press any key to close application.");
            Console.ReadKey();
        }
    }
}
