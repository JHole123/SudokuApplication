namespace SudokuEngine;

public class Segment
{
    public List<int> ValidValues = new(new int[] {1,2,3,4,5,6,7,8,9});
    public List<int> Tiles = new();
    public void UpdateValidValues(List<Tile> tiles)
    {
        ValidValues = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        foreach (int i in Tiles)
        {
            if (ValidValues.Contains(tiles[i].Value)) ValidValues.Remove(tiles[i].Value);
        }
    }

    public bool FindOnlyOnceCandidate(ref Board board, out int tileID)
    {
        tileID = -1;
        int occurences;
        for (int candidate = 1; candidate < 10; candidate++) {
            occurences = 0;
            foreach (int i in Tiles)
            {
                if (board[i].Candidates.Contains(candidate)) occurences++;
            } 
            if (occurences == 1)
            {
                foreach (int i in Tiles)
                {
                    if (board[i].Candidates.Contains(candidate)) { tileID = i; return true; }
                }
            }
        }
        return false;
    }

    //public bool FindSoleCandidate(ref Board board, out int tileID)
    //{
    //    tileID = -1;
    //    foreach (int i in Tiles)
    //    {
    //        if (board[i].Candidates.Count == 1) { tileID = i; return true; }
    //    }
    //    return false;
    //}
}

