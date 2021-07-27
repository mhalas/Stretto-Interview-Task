namespace Common.Tasks
{
    public class PrintCSVTask : IStrettoTask
    {
        private readonly string _input;

        public PrintCSVTask(string input)
        {
            _input = input;
        }

        public string Execute()
        {
            return _input;
        }
    }
}
