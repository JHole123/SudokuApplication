using System.Diagnostics;

namespace SudokuEngine;

public class Generator
{
    private readonly BoardAnalyser Analyser = new();
    private readonly BoardBacktracker Backtracker = new();
    private readonly Random R = new(Guid.NewGuid().GetHashCode());

    // gets a completed board and removes as many as it can from the board while still remaining solvable
    // difficulty parameter specifies the number of removal passes made
    public Board GetBoard(int difficulty)
    {
        Board b = GenerateTemplate();
        List<int> TilesTaken = new();
        int arg, valueRemoved;
        for (int i = 0; i < difficulty; i++)
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            Board c = b;
            if (Analyser.GetMove(ref b).Reason == "No Analytical Reason")
            {
                b[arg].Value = valueRemoved;
                continue;
            }
            TilesTaken.Add(arg);
        }
        return b;
    }

    // generates a full, completed sudoku board randomly
    private Board GenerateTemplate()
    {
        List<int> TilesFilled = new();
        Board b = new();
        int arg;

        // intitial random seeding
        for (int i = 0; i < 10; i++)
        {
            Debug.WriteLine("checkpoint 1");
            arg =  RestrictedRandomNext(36, TilesFilled);
            TilesFilled.Add(arg);
            b.UpdateSegmentValidValues();
            b[arg].Value =  RestrictedRandomNext(9, b[arg].GetCandidates(ref b), false, 1);
        }

        // puts down random tiles until the board can be solved
        do
        {
            arg = RestrictedRandomNext(81, TilesFilled);
            TilesFilled.Add(arg);
            b.UpdateSegmentValidValues();
            b[arg].Value = RestrictedRandomNext(9, b[arg].GetCandidates(ref b), false, 1);
        } while (!Backtracker.SolveBoard(b));
        return b;
    }

    // generates a random number from the set [0,n) ^ RestrictionSet
    // !SetIsRestrictive -> generates random number from set [0,n) ^ !RestrictionSet
    // offset applies to the set [0,n) to [0+offset,n+offset)
    private int RestrictedRandomNext(int ExclusiveMax, IEnumerable<int> RestrictionSet, bool SetIsRestrictive = true, int offset = 0)
    {
        var arg = R.Next(0+offset, ExclusiveMax+offset);
        while (RestrictionSet.Contains(arg) == SetIsRestrictive)
        {
            arg = R.Next(0 + offset, ExclusiveMax + offset);
        }
        return arg;
    }
}

