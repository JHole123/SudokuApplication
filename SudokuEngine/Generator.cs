using System.Diagnostics;

namespace SudokuEngine;

public class Generator
{
    private readonly BoardAnalyser ba = new();
    private readonly BoardBacktracker bb = new();
    private readonly Random R = new(Guid.NewGuid().GetHashCode());
    /*public Board GetBoard(int difficulty)
    {
        Board b = GenerateTemplate();
        //PrintBoard(b);
        List<int> TilesTaken = new();
        int arg;
        for (int i = 0; i < difficulty; i++)
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            b[arg].Value = 0;
            TilesTaken.Add(arg);
        }
        return b;
    }*/

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
            if (ba.GetMove(ref b).Reason == "No Analytical Reason")
            {
                b[arg].Value = valueRemoved;
                continue;
            }
            TilesTaken.Add(arg);
        }
        return b;
    }

    /*public Board GetEasyBoard()
    {
        Board b = GenerateTemplate();
        List<int> TilesTaken = new();
        int arg, valueRemoved;
        for (int i = 0; i < 30; i++)
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            Board c = b;
            if (ba.GetMove(ref b).Reason == "No Analytical Reason")
            {
                b[arg].Value = valueRemoved;
                continue;
            }
            TilesTaken.Add(arg);
        }
        return b;
    }

    public Board GetMediumBoard()
    {
        Board b = GenerateTemplate();
        List<int> TilesTaken = new();
        int arg, valueRemoved;
        for (int i = 0; i < 45; i++)
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            if (ba.GetMove(ref b).Reason == "No Analytical Reason")
            {
                b[arg].Value = valueRemoved;
                continue;
            }
            TilesTaken.Add(arg);
        }
        return b;
    }

    public Board GetHardBoard()
    {
        Board b = GenerateTemplate();
        List<int> TilesTaken = new();
        int arg = -1, valueRemoved = -1;
        while (ba.SolveBoard(b))
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            TilesTaken.Add(arg);
        }
        b[arg].Value = valueRemoved;
        return b;

        Board b = GenerateTemplate();
        List<int> TilesTaken = new();
        int arg, valueRemoved;
        for (int i = 0; i < 60; i++)
        {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            if (ba.GetMove(ref b).Reason == "No Analytical Reason")
            {
                b[arg].Value = valueRemoved;
                continue;
            }
            TilesTaken.Add(arg);
        }
        return b;
    } */

    private Board GenerateTemplate()
    {
        List<int> TilesFilled = new();
        Board b = new();
        int arg;

        // intitial random seeding that shouldnt be needed but apparently is
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
        } while (!bb.SolveBoard(b));
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

    private void PrintBoard(Board board)
    {
        for (int i = 0; i < 81; i++)
        {
            if (i + 1 %9 == 0) Debug.WriteLine($"{board.Tiles[i].Value}");
            else Debug.Write($"{board.Tiles[i].Value} ");
        }
    }
}

