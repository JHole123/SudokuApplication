using System.Diagnostics;

namespace SudokuEngine;

public class BoardBacktracker
{

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
        //Debug.WriteLine("SolveBoard invoked");
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

        // update candidates on this board
        board.UpdateSegmentValidValues();

        // try each candidate
        /*
        for (int i = 1; i < 10; i++)
        {
            board.UpdateSegmentValidValues();
            //var candidates = ManualCheck(ref board, trialTileId);
            var candidates = board.Tiles[trialTileId].GetCandidates(ref board);
            if (candidates.Contains(i))
            {
                Debug.WriteLine($"Candidates for {trialTileId}: {ConcatenateList(candidates)}");
                board.Tiles[trialTileId].Value = i;
                Debug.WriteLine($"Trying {i} at {trialTileId}");
                // recurse to try every tile
                if (SolveBoard(ref board)) return true;
                board.Tiles[trialTileId].Value = 0; 
                Debug.WriteLine("else condition reached"); 
            }
        }
        */

        foreach (int i in board.Tiles[trialTileId].GetCandidates(ref board))
        {
            //Debug.WriteLine($"Candidates for {trialTileId}:");
            board.Tiles[trialTileId].Value = i;
            //Debug.WriteLine($"Trying {i} at {trialTileId}");
            // recurse to try every tile
            if (SolveBoard(ref board)) return true;
            board.Tiles[trialTileId].Value = 0;
            //Debug.WriteLine("else condition reached");
        }

        return false;
    }

    // candidates are empty for a tile even when there is a valid candidate
    // this only seems to be a problem if the previous tile has been edited in some way

    private List<int> ManualCheck(ref Board board, int tileID)
    {
        List<int> candidates = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        int x = tileID % 9;
        int y = tileID / 9;
        int z = (y / 3 * 3) + (x / 3);
        int value;

        // x check
        for (int Y = 0; Y < 9; Y++)
        {
            value = board.Tiles[x + (Y * 9)].Value;
            candidates.Remove(value);
        }

        // y check
        for (int X = 0; X < 9; X++)
        {
            value = board.Tiles[X + (y * 9)].Value;
            candidates.Remove(value);
        }

        // z check
        for (int Y = z/9; Y < (z/9) + 3; Y++)
        {
            for (int X = z % 9; X < (z % 9) + 3; X++)
            {
                value = board.Tiles[X + (Y * 9)].Value;
                candidates.Remove(value);
            }
        }

        return candidates;
    }

    private string ConcatenateList(List<int> t)
    {
        if (t.Count == 0) return "{}";
        string arg = "{";
        foreach (int i in t)
        {
            arg += i.ToString()+", ";
        }
        return arg[0..^2] + "}";
    }

}
