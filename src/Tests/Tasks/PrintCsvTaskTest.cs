using Common.Tasks;
using NUnit.Framework;

namespace Tests.Tasks
{
    public class PrintCsvTaskTest
    {
        [TestCase("test")]
        public void Task_PrintCsv(string textToPrint)
        {
            PrintCSVTask task = new PrintCSVTask(textToPrint);
            var result = task.Execute();

            Assert.AreEqual(textToPrint, result);
        }
    }
}
