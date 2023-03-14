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
        if (!Directory.Exists(GeneratorDir)) GenerateTemplates();
        return default!;
    }

    private void GenerateTemplates()
    {
        Debug.WriteLine("GenerateTemplates called");
        Directory.CreateDirectory(GeneratorDir);



    }

    private void GenerateTemplate()
    {
        List<int> TilesFilled = new();
        Board b = new();
        var arg = RestrictedRandomNext(81, TilesFilled);
        b[arg].Value = RestrictedRandomNext(9, b[arg].Candidates, false, 1);
    }

    // generates a random number from the set [0,n) ^ RestrictionSet
    // !SetIsRestrictive -> generates random number from set [0,n) ^ !RestrictionSet
    // offset applies to the set [0,n) to [0+offset,n+offset)
    private int RestrictedRandomNext(int ExclusiveMax, IEnumerable<int> RestrictionSet, bool SetIsRestrictive = true, int offset = 0)
    {
        var arg = R.Next(0+offset, ExclusiveMax+offset);
        while (RestrictionSet.Contains(arg) == SetIsRestrictive)
        {
            arg = (arg++ % ExclusiveMax) +offset;
        }
        return arg;
    }
    // FIX:: loops infinitely causing program freeze in certain situations
}

