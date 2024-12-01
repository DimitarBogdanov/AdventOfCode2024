namespace AdventOfCode2024;

public sealed class Day1Puzzle2 : IAdventProblem
{
    public int DayNumber => 1;
    
    public void Solve(string input)
    {
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int[] leftList = new int[lines.Length];
        int[] rightList = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] nums = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftList[i] = Int32.Parse(nums[0]);
            rightList[i] = Int32.Parse(nums[1]);
        }

        long totalSimilarityScore = 0;

        foreach (int locationNumber in leftList)
        {
            int count = rightList.Count(x => x == locationNumber);
            totalSimilarityScore += locationNumber * count;
        }

        Console.WriteLine(totalSimilarityScore);
    }
}