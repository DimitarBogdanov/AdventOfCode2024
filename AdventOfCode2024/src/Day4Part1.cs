namespace AdventOfCode2024;

public sealed class Day4Part1 :IAdventProblem
{
    public int DayNumber => 4;
    
    public void Solve(string input)
    {
        string[] lines = input.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
        }

        int h = lines.Length;
        int w = lines[0].Length;
        char[,] matrix = new char[w, h];
        int lineCounter = 0;
        foreach (string line in lines)
        {
            for (int i = 0; i < w; i++)
            {
                matrix[i, lineCounter] = line[i];
            }

            lineCounter++;
        }

        List<string> strings = new();
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                // Normal
                if (IsWithinBounds(h, w, x + 3, y))
                {
                    strings.Add($"{matrix[x, y]}{matrix[x + 1, y]}{matrix[x + 2, y]}{matrix[x + 3, y]}");
                }
                
                // Straight down
                if (IsWithinBounds(h, w, x, y + 3))
                {
                    strings.Add($"{matrix[x, y]}{matrix[x, y + 1]}{matrix[x, y + 2]}{matrix[x, y + 3]}");
                }
                
                // Bottom-upper diagonal
                if (IsWithinBounds(h, w, x + 3, y + 3))
                {
                    strings.Add($"{matrix[x, y]}{matrix[x + 1, y + 1]}{matrix[x + 2, y + 2]}{matrix[x + 3, y + 3]}");
                }
                
                // Upper-bottom diagonal
                if (IsWithinBounds(h, w, x + 3, y - 3))
                {
                    strings.Add($"{matrix[x, y]}{matrix[x + 1, y - 1]}{matrix[x + 2, y - 2]}{matrix[x + 3, y - 3]}");
                }
            }
        }

        int count = strings.Count(x => x is "XMAS" or "SAMX");
        Console.WriteLine(count);
    }

    private static bool IsWithinBounds(int height, int width, int x, int y)
    {
        return x >= 0 && x < width
                      && y >= 0 && y < height;
    }
}