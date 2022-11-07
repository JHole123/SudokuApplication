namespace SudokuEngine;

public class Tile
{
    public int TileID { get; set; }
    public int Value { get; set; }
    public List<int> Candidates = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    public Tile(int tileID)
    {
        TileID = tileID;
        //Value = 0;
    }
    public int this[int n]
    {
        get { return Candidates[n]; }
        set { Candidates[n] = value; }
    }
    public int[] GetSegments()
    {
        int x = TileID % 9;
        int y = TileID / 9;
        int z = (y / 3 * 3) + (x / 3); // do not worry about the legendary divide then multiply strat it is essential
        return new int[] { x, y + 9, z + 18 };
    }

    // prunes candidates list for any it can, then returns the candidate list
    public int[] GetCandidates(ref Board board)
    {
        int[] segmentIDs = GetSegments();
        Segment[] segments = new Segment[] { board.Segments[segmentIDs[0]], board.Segments[segmentIDs[1]], board.Segments[segmentIDs[2]] };
        foreach (Segment sgmnt in segments)
        {
            foreach (int i in Candidates)
            {
                if (!sgmnt.ValidValues.Contains(i)) Candidates.Remove(i);
            }
        }
        return Candidates.ToArray();
    }
    // list of references to segments that contain this tile
}