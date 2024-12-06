namespace AdventOfCode2024;

public sealed class Day6Part2 : IAdventProblem
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

        int initialGpx = guardPosX, initialGpy = guardPosY;

        const int dirUp = 0; 
        const int dirDown = 1; 
        const int dirLeft = 2; 
        const int dirRight = 3;

        int counter = 0;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (map[x, y] || (x == initialGpx && y == initialGpy))
                {
                    continue;
                }

                map[x, y] = true;

                List<(int, int)> turningPositions = [];
                int direction = dirUp;
                bool wentOutOfBounds = false;
                bool hasCycle = false;

                // how many times the guard goes in a circle
                // until we determine that a cycle exists 100%
                // this can be much lower, but the larger number
                // is convenient to make sure 100% correctness
                const int necessaryRotations = 10;
                
                guardPosX = initialGpx;
                guardPosY = initialGpy;

                while (!wentOutOfBounds && !hasCycle)
                {
                    switch (direction)
                    {
                        case dirUp:
                            if (guardPosY - 1 < 0)
                            {
                                wentOutOfBounds = true;
                            }
                            else if (map[guardPosX, guardPosY - 1])
                            {
                                if (turningPositions.Count(t => t == (guardPosX, guardPosY)) > necessaryRotations)
                                {
                                    hasCycle = true;
                                }
                                turningPositions.Add((guardPosX, guardPosY));
                                direction = dirRight;
                            }
                            else
                            {
                                guardPosY--;
                            }

                            break;
                        case dirDown:
                            if (guardPosY + 1 == h)
                            {
                                wentOutOfBounds = true;
                            }
                            else if (map[guardPosX, guardPosY + 1])
                            {
                                if (turningPositions.Count(t => t == (guardPosX, guardPosY)) > necessaryRotations)
                                {
                                    hasCycle = true;
                                }
                                turningPositions.Add((guardPosX, guardPosY));
                                direction = dirLeft;
                            }
                            else
                            {
                                guardPosY++;
                            }

                            break;
                        case dirLeft:
                            if (guardPosX - 1 < 0)
                            {
                                wentOutOfBounds = true;
                            }
                            else if (map[guardPosX - 1, guardPosY])
                            {
                                if (turningPositions.Count(t => t == (guardPosX, guardPosY)) > necessaryRotations)
                                {
                                    hasCycle = true;
                                }
                                turningPositions.Add((guardPosX, guardPosY));
                                direction = dirUp;
                            }
                            else
                            {
                                guardPosX--;
                            }

                            break;
                        case dirRight:
                            if (guardPosX + 1 == w)
                            {
                                wentOutOfBounds = true;
                            }
                            else if (map[guardPosX + 1, guardPosY])
                            {
                                if (turningPositions.Count(t => t == (guardPosX, guardPosY)) > necessaryRotations)
                                {
                                    hasCycle = true;
                                }
                                turningPositions.Add((guardPosX, guardPosY));
                                direction = dirDown;
                            }
                            else
                            {
                                guardPosX++;
                            }

                            break;
                    }
                }

                if (hasCycle && !wentOutOfBounds)
                {
                    counter++;
                }

                map[x, y] = false;
            }
        }

        Console.WriteLine(counter); // do not ask why, i don't know!
    }
}