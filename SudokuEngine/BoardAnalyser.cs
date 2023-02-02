namespace SudokuEngine;

public class BoardAnalyser
{
    public Move GetMove(ref Board board)
    {
        // Checks if any tile has a sole candidate; that tile can be filled
        foreach (Tile t in board.Tiles)
        {
            if (t.Candidates.Count == 1) return new Move("Sole Candidate", t[0], t.TileID);
        }

        return new Move("Sole Candidate", board.Tiles[0][0], board.Tiles[0].TileID);
    }


}

