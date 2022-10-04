namespace SudokuApplication;

public class Move
{
    // reference to tile being changed
    public int NewValue;
    // list of references to relevant segments
    public AnalyticalReason Reason;
}

