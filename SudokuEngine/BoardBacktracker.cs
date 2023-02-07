namespace SudokuEngine;

public class BoardBacktracker
{
    private Board MainBoard;
    public BoardBacktracker(Board board)
    {
        MainBoard = board;
    }

    // needs fixing
    public bool SolveBoard()
    {
        int TrialTileID = -1;
        Board trialBoard = MainBoard;

        // find the next tile that is not filled
        foreach (Tile t in trialBoard.Tiles)
        {
            if (t.Value == 0)
            {
                TrialTileID = t.TileID;
                break;
            }
        }

        // if the board is already complete then return true
        if (TrialTileID == -1) return true;

        // check each candidate
        for (int i = 1; i < 10; i++)
        {
            if (trialBoard[TrialTileID].GetCandidates(ref trialBoard).Contains(i))
            {
                trialBoard[TrialTileID].Value = i;
                if (SolveBoard()) return true;
                else trialBoard[TrialTileID].Value = 0;
            }
        }


        return false;
    }
}
