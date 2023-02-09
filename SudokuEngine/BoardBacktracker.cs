namespace SudokuEngine;

public class BoardBacktracker
{
    private Board MainBoard;
    public BoardBacktracker(Board board)
    {
        MainBoard = board;
    }

    // needs fixing
    /*public bool SolveBoard(out Board trialBoard)
    {
        int TrialTileID = -1;
        trialBoard = MainBoard;

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
                if (SolveBoard(out Board tb)) { trialBoard = tb; return true; }
                //else trialBoard[TrialTileID].Value = 0;
            }
        }

        return false;
    }*/

    public bool SolveBoard(ref Board board)
    {
        int trialTileId = -1;

        // find empty tile
        foreach (Tile t in board.Tiles)
        {
            if (t.Value == 0)
            {
                trialTileId = t.TileID;
                break;
            }
        }

        // if every tile is full return true
        if (trialTileId == -1) return true;

        // try each candidate
        for (int ican = 1; ican < 10; ican++)
        {
            var candidates = board.Tiles[trialTileId].GetCandidates(ref board);
            if (candidates.Contains(ican))
            {
                board.Tiles[trialTileId].Value = ican;
                // recurse to try every tile
                if (SolveBoard(ref board)) return true;
                else board.Tiles[trialTileId].Value = 0;
            }
        }
        return false;
    }

}
