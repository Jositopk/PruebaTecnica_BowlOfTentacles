using System;

public interface IPuzzle
{
    string PuzzleId { get; }
    bool IsSolved { get; }
    event Action<IPuzzle> OnSolved;
}