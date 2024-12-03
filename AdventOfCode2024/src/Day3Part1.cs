using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public sealed class Day3Part1 : IAdventProblem
{
    public int DayNumber => 3;

    public void Solve(string input)
    {
        Regex regex = new("mul\\((?<num1>[0-9]+),(?<num2>[0-9]+)\\)");
        MatchCollection matches = regex.Matches(input);

        int total = 0;
        foreach (Match match in matches)
        {
            Group numGroup1 = match.Groups["num1"];
            Group numGroup2 = match.Groups["num2"];

            int num1 = Int32.Parse(numGroup1.Value);
            int num2 = Int32.Parse(numGroup2.Value);

            total += (num1 * num2);
        }

        Console.WriteLine(total);
    }
}