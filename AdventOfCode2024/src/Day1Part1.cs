namespace AdventOfCode2024;

public sealed class Day1Part1 : IAdventProblem
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

        long totalDist = 0;
        
        for (int i = 0; i < leftList.Length; i++)
        {
            int idxOfMinLeft = IndexOfMin(leftList);
            int idxOfMinRight = IndexOfMin(rightList);
            
            int numLeft = leftList[idxOfMinLeft];
            int numRight = rightList[idxOfMinRight];
            
            int distance = Math.Abs(numLeft - numRight);
            totalDist += distance;
            
            leftList[idxOfMinLeft] = Int32.MaxValue;
            rightList[idxOfMinRight] = Int32.MaxValue;
        }

        Console.WriteLine(totalDist);
    }

    private static int IndexOfMin(int[] array)
    {
        int min = array.Min();
        int idx = Array.IndexOf(array, min);
        return idx;
    }
}