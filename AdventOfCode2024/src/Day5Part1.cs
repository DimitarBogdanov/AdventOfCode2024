namespace AdventOfCode2024;

public sealed class Day5Part1 : IAdventProblem
{
    public int DayNumber => 5;
    
    public void Solve(string input)
    {
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        List<(int, int)> rules
            = lines
                .Where(x => x.Contains('|'))
                .Select(x => x.Split('|'))
                .Select(x => (Int32.Parse(x[0]), Int32.Parse(x[1])))
                .ToList();

        List<List<int>> updates
            = lines
                .Where(x => !x.Contains('|'))
                .Select(x => x.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Select(Int32.Parse).ToList())
                .ToList();

        long total = 0;
        foreach (List<int> upd in updates)
        {
            if (IsValid(rules, upd))
            {
                total += upd[upd.Count / 2];
            }
        }

        Console.WriteLine(total);
    }

    private static bool IsValid(List<(int, int)> rules, List<int> update)
    {
        for (int i = 0; i < update.Count; i++)
        {
            int page1 = update[i];
            for (int j = i+1; j < update.Count; j++)
            {
                int page2 = update[j];
                foreach ((int, int) rule in rules)
                {
                    if (rule.Item1 == page2 && rule.Item2 == page1)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}