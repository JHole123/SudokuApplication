namespace SudokuEngine;

public class Move
{
    public int TileID;
    public int NewValue;
    public string Reason;

    public Move(string reason, int value, int tileID)
    {
        TileID = tileID;
        NewValue = value;
        Reason = reason;
    }
}
