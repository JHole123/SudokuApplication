using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEngine;

public class HintsHandler
{
    public Dictionary<string, string> HintsMeaning = new();
    public HintsHandler()
    {
        PopulateDictionary();
    }
    public void PopulateDictionary()
    {
        HintsMeaning.Add("Sole Candidate", "On your board, there is at least one tile with only one candidate. This means the value of that tile must be equal to the candidate. This is the simplest form of finding a new value.");
        HintsMeaning.Add("No Analytical Move", "On your board, there are no moves that could be reasonably found using analysis.");
        HintsMeaning.Add("Cross Hatch Scan", "some text here");
        //foreach (string s in File.ReadAllLines(@"M:\Pogramming\SudokuApplication\SudokuEngine\Hints.txt"))
        //    HintsMeaning.Add(s.Split(":")[0], s.Split(":")[1]);
    }
}

