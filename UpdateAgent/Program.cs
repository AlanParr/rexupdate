namespace UpdateAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            var agent = new Agent(args);
            agent.Execute();
        }
    }
}
