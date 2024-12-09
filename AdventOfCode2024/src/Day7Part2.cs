using System.Text;

namespace AdventOfCode2024;

public sealed class Day7Part2 : IAdventProblem
{
    enum NextMode
    {
        Add,
        Mul,
        Concat
    }

    public int DayNumber => 7;

    public void Solve(string input)
    {
        string[] lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        long totalSum = 0;
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            long result = Int64.Parse(parts[0]);
            string[] eqParts =
                parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<string> variants = BuildVariants(eqParts, eqParts.Length);
            if (HasSolution(result, variants))
            {
                totalSum += result;
            }
        }
        
        Console.WriteLine(totalSum);
    }

    private static bool HasSolution(long result, List<string> variants)
    {
        foreach (string eq in variants)
        {
            long total = 0;
            StringBuilder currentNum = new();
            NextMode nextMode = NextMode.Concat;
            for (int i = 0; i < eq.Length; i++)
            {
                char c = eq[i];
                if (Char.IsDigit(c))
                {
                    currentNum.Append(c);
                }
                else
                {
                    long num = Int64.Parse(currentNum.ToString());
                    switch (nextMode)
                    {
                        case NextMode.Add:
                            total += num;
                            break;
                        case NextMode.Mul:
                            total *= num;
                            break;
                        case NextMode.Concat:
                        {
                            string numStr = total.ToString() + currentNum;
                            total = Int64.Parse(numStr);
                            break;
                        }
                    }

                    currentNum.Clear();
                    nextMode = c switch
                    {
                        '+' => NextMode.Add,
                        '*' => NextMode.Mul,
                        '|' => NextMode.Concat
                    };
                    if (c == '|')
                    {
                        i++; // to skip second char
                    }
                }
            }

            if (currentNum.Length != 0)
            {
                long num = Int64.Parse(currentNum.ToString());
                switch (nextMode)
                {
                    case NextMode.Add:
                        total += num;
                        break;
                    case NextMode.Mul:
                        total *= num;
                        break;
                    case NextMode.Concat:
                    {
                        string numStr = total.ToString() + currentNum;
                        total = Int64.Parse(numStr);
                        break;
                    }
                }
            }

            if (total == result)
            {
                return true;
            }
        }

        return false;
    }

    private static List<string> BuildVariants(string[] eqParts, int length)
    {
        List<string> results = [];

        if (length == 2)
        {
            results.Add($"{eqParts[0]}+{eqParts[1]}");
            results.Add($"{eqParts[0]}*{eqParts[1]}");
            results.Add($"{eqParts[0]}||{eqParts[1]}");
        }
        else
        {
            List<string> previousResults = BuildVariants(eqParts, length - 1);
            string newPart = eqParts[length - 1];
            foreach (string res in previousResults)
            {
                results.Add($"{res}+{newPart}");
                results.Add($"{res}*{newPart}");
                results.Add($"{res}||{newPart}");
            }
        }

        return results;
    }
}