using System.Diagnostics;

namespace SudokuEngine;

public class Generator
{
    //private string GeneratorDir = @"M:\Pogramming\SudokuApplication\SudokuEngine\GenTemplates\";
    private string GeneratorDir = @$"{Directory.GetCurrentDirectory()}..\..\..\..\..\SudokuEngine\GenTemplates\";
    private BoardBacktracker bb = new();
    private Random R = new(Guid.NewGuid().GetHashCode());
    public Board GetEasyBoard()
    {
        GenerateTemplate();
        if (!Directory.Exists(GeneratorDir)) GenerateTemplates();
        Debug.WriteLine("T");
        return default!;
    }
    public Board GetMediumBoard()
    {
        if (!Directory.Exists(GeneratorDir)) GenerateTemplates();
        return default!;
    }
    
    public Board GetHardBoard()
    {
        Board b = GenerateTemplate();
        Board c = b;
        List<int> TilesTaken = new();
        int arg, valueRemoved;
        // when bb.SolveBoard(ref c) is run, c becomes fully solved
        // therefore it is necessary to allow c to be a copy of b, so that c will be solved but b will not be
        // otherwise b would just continuously take tiles, check if it can be solved (and solving it in the process), and then take another first tile
        while (bb.SolveBoard(ref c)) {
            arg = RestrictedRandomNext(81, TilesTaken);
            valueRemoved = b[arg].Value;
            b[arg].Value = 0;
            TilesTaken.Add(arg);
            c = b;
        }
        
        // when the loop finished, it has taken enough squares to not allow solving. add the last one in so it *is* solvable
        b[arg].Value = valueRemoved;
        
        
        /*Board b = GenerateTemplate();
        PrintBoard(b);
        List<int> TilesTaken = new();
        int arg;
        do
        {
            //Debug.WriteLine("checkmark 1");
            arg = R.Next(0, 81);
            b[arg].Value = 0;
        } while (bb.SolveBoard(ref b));
        //Debug.WriteLine("Checkmark 2");
        //if (!Directory.Exists(GeneratorDir)) GenerateTemplates();*/
        return b;
    }

    private void GenerateTemplates()
    {
        Debug.WriteLine("GenerateTemplates called");
        Directory.CreateDirectory(GeneratorDir);



    }

    private Board GenerateTemplate()
    {
        List<int> TilesFilled = new();
        Board b = new();
        int arg;

        // intitial random seeding that shouldnt be needed but apparently is
        for (int i = 0; i < 15; i++)
        {
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
        } while (!bb.SolveBoard(ref b));
        return b;
    }

    private int GenerateRandomCandidate(List<int> Candidates)
    {
        int arg;
        do
        {
            arg = R.Next(1, 10);
        } while (Candidates.Contains(arg));
        return arg;
    }

    private int GenerateRandomTile(List<int> TilesFilled)
    {
        int arg;
        do
        {
            arg = R.Next(0, 81);
        } while (TilesFilled.Contains(arg));
        return arg;
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

