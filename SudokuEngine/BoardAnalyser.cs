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
            if (seg.FindOnlyOnceCandidate(ref board, out int tileID)) return new Move("Cross Hatch Scan", board[tileID].Value, tileID);
        }

        // Generalised Method Rule
        


        // cross hatch scanning
        //foreach (Segment seg in board.Segments)
        //{
        //    var arg = IsFoundOnceSuper(ref board, seg, out int val);
        //    if (arg != -1) return new Move("Cross Hatch Scan", val, arg);
        //}

        // if no move is found, return that no move is found
        return new Move("No Analytical Move", board.Tiles[0][0], board.Tiles[0].TileID);
    }

    // Checks to see if there is only one instance of every candidate in a segment
    //private int IsFoundOnceSuper(ref Board board, Segment seg, out int value)
    //{
    //    value = -1;
    //    int tileID = -1; // -1 meaning no tile found
    //    for (int i = 1; i < 10; i++) {
    //        if (IsFoundOnce(ref board, seg, i))
    //        {
    //            // go through each tile in the segment to find which tile has the candidate
    //            foreach (int tileId in seg.Tiles)
    //            {
    //                if (board[tileId].Candidates.Contains(i)) { tileID = tileId; value = board[tileID].Value; break; }
    //            }
    //            break;
    //        }
    //    }
    //    return tileID;
    //}
        
    // Checks to see if there is only one instance of a given candidate in a segment
    //private bool IsFoundOnce(ref Board board, Segment seg, int candidate)
    //{
    //    int instances = 0;
    //    foreach (int tileID in seg.Tiles)
    //    {
    //        if (board[tileID].Candidates.Contains(candidate)) instances++;
    //    }
    //    return instances == 1;
    //}
}

