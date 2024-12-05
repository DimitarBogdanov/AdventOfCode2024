using System.Diagnostics;

namespace AdventOfCode2024;

public sealed class Day5Part2 : IAdventProblem
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
        int idx = 0;
        float max = updates.Count;
        foreach (List<int> upd in updates)
        {
            Console.WriteLine($"Upd {++idx} / {max}");
            if (IsValid(rules, upd, out _))
            {
                continue;
            }
            
            while (!IsValid(rules, upd, out int vIdx))
            {
                int num = upd[vIdx];
                upd.RemoveAt(vIdx);
                upd.Insert(vIdx-1, num);
            }

            total += upd[upd.Count / 2];
        }

        Console.WriteLine(total);
    }

    private static bool IsValid(List<(int, int)> rules, List<int> update, out int violatingIdx)
    {
        for (int i = 0; i < update.Count; i++)
        {
            int page1 = update[i];
            for (int j = i + 1; j < update.Count; j++)
            {
                int page2 = update[j];
                foreach ((int, int) rule in rules)
                {
                    if (rule.Item1 == page2 && rule.Item2 == page1)
                    {
                        violatingIdx = j;
                        return false;
                    }
                }
            }
        }

        violatingIdx = -1;
        return true;
    }
}