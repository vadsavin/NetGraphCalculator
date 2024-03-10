using NetGraphSolver;

namespace NetGraphCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputFile = "input.txt";
            var jobs = GraphJobParser.ParseGraphJobs(File.ReadAllText(inputFile));

            var solver = new GraphSolver(jobs);
            solver.CalculateAll();

            Console.WriteLine(solver.CalculateCriticalRoute());
            Console.WriteLine(string.Join(", ", solver.GetCriticalRoute()));
        }
    }
}