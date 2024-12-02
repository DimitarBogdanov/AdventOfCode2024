namespace AdventOfCode2024;

public sealed class Day2Part1 : IAdventProblem
{
    public int DayNumber => 2;
    
    public void Solve(string input)
    {
        string[] reportLines = input.Split('\n');
        List<int>[] reports
            = reportLines
                .Select(x => x.Split(' '))
                .Select(x => x.Select(Int32.Parse))
                .Select(x => x.ToList())
                .ToArray();

        int countOfSafeReports = 0;

        foreach (List<int> report in reports)
        {
            if (report.Count < 2)
            {
                continue;
            }

            bool increasing = report[0] < report[1];

            bool safe = true;
            for (int i = 1; i < report.Count; i++)
            {
                int value = report[i];
                int last = report[i - 1];
                
                int dist = Math.Abs(value - last);
                if (dist is < 1 or > 3)
                {
                    // Failed rule two about difference
                    safe = false;
                    break;
                }

                if (increasing && value < last)
                {
                    // Failed increasing order
                    safe = false;
                    break;
                }

                if (!increasing && value > last)
                {
                    // Failed decreasing order
                    safe = false;
                    break;
                }
            }

            if (safe)
            {
                countOfSafeReports++;
            }
        }

        Console.WriteLine(countOfSafeReports);
    }
}