namespace AdventOfCode2024;

public interface IAdventProblem
{
    public int DayNumber { get; }
    
    void Solve(string input);
}