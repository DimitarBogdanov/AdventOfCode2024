namespace AdventOfCode2024;

public sealed class Day4Part2 : IAdventProblem
{
    public int DayNumber => 4;
    
    public void Solve(string input)
    {
        char[][] masks =
        {
            new[] {
                'M', ' ', 'M',
                ' ', 'A', ' ',
                'S', ' ', 'S'
            },
            new[] {
                'S', ' ', 'M',
                ' ', 'A', ' ',
                'S', ' ', 'M'
            },
            new[] {
                'S', ' ', 'S',
                ' ', 'A', ' ',
                'M', ' ', 'M'
            },
            new[] {
                'M', ' ', 'S',
                ' ', 'A', ' ',
                'M', ' ', 'S'
            },
        };
        
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

        int count = 0;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                // Normal
                if (IsWithinBounds(h, w, x + 2, y + 2))
                {
                    foreach (char[] mask in masks)
                    {
                        if (MaskApplies(matrix, x, y, mask))
                        {
                            count++;
                        }
                    }
                }
            }
        }

        Console.WriteLine(count);
    }

    private static bool MaskApplies(char[,] matrix, int x, int y, char[] mask)
    {
        return matrix[x, y] == mask[0]
               && matrix[x + 2, y] == mask[2]
               && matrix[x + 1, y + 1] == mask[4]
               && matrix[x, y + 2] == mask[6]
               && matrix[x + 2, y + 2] == mask[8];
    }
    
    private static bool IsWithinBounds(int height, int width, int x, int y)
    {
        return x >= 0 && x < width
                      && y >= 0 && y < height;
    }
}