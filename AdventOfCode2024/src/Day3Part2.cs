using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public sealed class Day3Part2 : IAdventProblem
{
    public int DayNumber => 3;

    public void Solve(string input)
    {
        Regex enableRegex = new("do\\(\\)");
        Regex disableRegex = new("don't\\(\\)");
        Regex mainRegex = new("mul\\((?<num1>[0-9]+),(?<num2>[0-9]+)\\)");
        List<int> enablers = enableRegex.Matches(input).Select(x => x.Index).ToList();
        List<int> disablers = disableRegex.Matches(input).Select(x => x.Index).ToList();
        MatchCollection matches = mainRegex.Matches(input);
        
        enablers.Insert(0, 0);

        int total = 0;
        foreach (Match match in matches)
        {
            if (!IsEnabled(enablers, disablers, match.Index))
            {
                continue;
            }
            
            Group numGroup1 = match.Groups["num1"];
            Group numGroup2 = match.Groups["num2"];

            int num1 = Int32.Parse(numGroup1.Value);
            int num2 = Int32.Parse(numGroup2.Value);

            total += (num1 * num2);
        }

        Console.WriteLine(total);
    }

    private bool IsEnabled(List<int> enablers, List<int> disablers, int pos)
    {
        int closestEnable = GetLastOrMinValue(enablers, pos);
        int closestDisable = GetLastOrMinValue(disablers, pos);

        return closestEnable > closestDisable;
    }

    private int GetLastOrMinValue(List<int> list, int barrier)
    {
        try
        {
            return list.Last(x => x < barrier);
        }
        catch
        {
            return Int32.MinValue;
        }
    }
    
}