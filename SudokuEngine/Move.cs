namespace SudokuEngine;

public class Move
{
    public int TileID;
    public int NewValue;
    public AnalyticalReason Reason;

    public Move(AnalyticalReason ar, int value, int tileID)
    {
        TileID = tileID;
        NewValue = value;
        Reason = ar;
    }
}
