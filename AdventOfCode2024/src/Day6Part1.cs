namespace AdventOfCode2024;

public sealed class Day6Part1 : IAdventProblem
{
    public int DayNumber => 6;
    
    public void Solve(string input)
    {
        string[] lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        int w = lines[0].Length;
        int h = lines.Length;

        int guardPosX = 0, guardPosY = 0;

        bool[,] map = new bool[w, h];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                char ch = lines[i][j];
                map[j, i] = ch == '#';
                if (ch == '^')
                {
                    guardPosX = j;
                    guardPosY = i;
                }
            }
        }

        const int dirUp = 0; 
        const int dirDown = 1; 
        const int dirLeft = 2; 
        const int dirRight = 3;

        List<(int, int)> walkedPositions = [(guardPosX, guardPosY)];
        int direction = dirUp;
        bool finished = false;
        while (!finished)
        {
            switch (direction)
            {
                case dirUp:
                    if (guardPosY - 1 < 0)
                    {
                        finished = true;
                    }
                    else if (map[guardPosX, guardPosY - 1])
                    {
                        direction = dirRight;
                    }
                    else
                    {
                        guardPosY--;
                        walkedPositions.Add((guardPosX, guardPosY));
                    }
                    break;
                case dirDown:
                    if (guardPosY + 1 == h)
                    {
                        finished = true;
                    }
                    else if (map[guardPosX, guardPosY + 1])
                    {
                        direction = dirLeft;
                    }
                    else
                    {
                        guardPosY++;
                        walkedPositions.Add((guardPosX, guardPosY));
                    }
                    break;
                case dirLeft:
                    if (guardPosX - 1 < 0)
                    {
                        finished = true;
                    }
                    else if (map[guardPosX - 1, guardPosY])
                    {
                        direction = dirUp;
                    }
                    else
                    {
                        guardPosX--;
                        walkedPositions.Add((guardPosX, guardPosY));
                    }
                    break;
                case dirRight:
                    if (guardPosX + 1 == w)
                    {
                        finished = true;
                    }
                    else if (map[guardPosX + 1, guardPosY])
                    {
                        direction = dirDown;
                    }
                    else
                    {
                        guardPosX++;
                        walkedPositions.Add((guardPosX, guardPosY));
                    }
                    break;
            }
        }

        IEnumerable<(int, int)> distinct = walkedPositions.Distinct();
        
        Console.WriteLine(distinct.Count());
    }
}