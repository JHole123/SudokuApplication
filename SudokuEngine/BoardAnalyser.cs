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

        // Cross Hatch Scanning
        foreach (Segment seg in board.Segments)
        {
            if (seg.FindOnlyOnceCandidate(ref board, out int tileID)) 
                return new Move("Cross Hatch Scan", board[tileID].Value, tileID);
        }

        // if no move is found, return that no move is found
        return new Move("No Analytical Move", board.Tiles[0][0], board.Tiles[0].TileID);
    }
}

