namespace SudokuEngine;

public class BoardBacktracker
{
    public bool SolveBoard(Board board)
    {
        // invalid tile id to check whether there are any valid tiles to change it to
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

        // if every tile is full return true (board is completed)
        if (trialTileId == -1) return true;

        // update candidates on this board
        board.UpdateSegmentValidValues();

        foreach (int i in board.Tiles[trialTileId].GetCandidates(ref board))
        {
            board.Tiles[trialTileId].Value = i;
            // recurse to try every tile
            if (SolveBoard(board)) return true;
            board.Tiles[trialTileId].Value = 0;
        }

        return false;
    }

}
