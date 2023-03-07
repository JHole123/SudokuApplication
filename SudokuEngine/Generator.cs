using System.Diagnostics;

namespace SudokuEngine;

public class Generator
{
    private string GeneratorDir = @"M:\Pogramming\SudokuApplication\SudokuEngine\GenTemplates\";
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
}

