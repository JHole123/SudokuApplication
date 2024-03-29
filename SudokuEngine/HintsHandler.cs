﻿namespace SudokuEngine;

public class HintsHandler
{
    public Dictionary<string, string> HintsMeaning = new();
    public HintsHandler()
    {
        PopulateDictionary();
    }
    public void PopulateDictionary()
    {
        foreach (string s in 
            File.ReadAllLines(@"M:\Pogramming\SudokuApplication\SudokuEngine\Hints.txt"))
            HintsMeaning.Add(s.Split(":")[0], s.Split(":")[1]);
    }
}
