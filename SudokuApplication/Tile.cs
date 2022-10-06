

namespace SudokuApplication;

public class Tile
{
    public int TileID { get; set; }
    public int Value { get; set; }
    public Tile(int tileID)
    {
        TileID = tileID;
    }
    public int[] GetSegments()
    {
        int x = TileID % 9;
        int y = TileID / 9;
        int z = (y / 3 * 3) + (x / 3); // do not worry about the legendary divide then multiply strat it is essential
        return new int[] { x, y+9, z+18 };
    }
    public int[] GetCandidates(ref Board board)
    {
        List<int> candidates = new List<int>(new int[] {1,2,3,4,5,6,7,8,9});
        int[] segmentIDs = GetSegments();
        Segment[] segments = new Segment[] { board.Segments[segmentIDs[0]], board.Segments[segmentIDs[1]], board.Segments[segmentIDs[2]] };
        foreach (Segment sgmnt in segments)
        {
            foreach (int i in candidates)
            {
                if (!sgmnt.ValidValues.Contains(i)) candidates.Remove(i);
            }
        }
        return candidates.ToArray();
    }
    // list of references to segments that contain this tile
}

