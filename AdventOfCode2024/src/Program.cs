namespace AdventOfCode2024;

public static class Program
{
    public static void Main()
    {
        IAdventProblem problem = new Day7Part2();
        string? input = GetInput(problem);
        if (input == null)
        {
            Console.WriteLine("Input file not found, please add it to the executable directory with the name 'input_X.txt', where X = the day number");
            return;
        }
        
        problem.Solve(input);
    }

    private static string? GetInput(IAdventProblem forProblem)
    {
        int dayNumber = forProblem.DayNumber;
        string targetPath = Path.Combine(Environment.CurrentDirectory, $"input_{dayNumber}.txt");
        return File.Exists(targetPath)
            ? File.ReadAllText(targetPath)
            : null;
    }
}