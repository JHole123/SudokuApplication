using System.Diagnostics;

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
    // returns a int[] that contains the segment IDs of the 3 segments this tile is in
    public int[] GetSegments()
    {
        int x = TileID % 9;
        int y = TileID / 9;
        int z = (y / 3 * 3) + (x / 3); // it rounds down to the nearest n since it is integer division (divides then rounds down)
                                       // this maths finds the "anchor point" of a 3x3 segment in the top left of the segment
        return new int[] { x, y+9, z + 18 }; // within Segments list: horizontal segments indices [0,8]; vertical [9,17]; 3x3 [18,26] therefore offsets of 0, 9, 18
    }

    // prunes candidates list for any it can, then returns the candidate list
    public List<int> GetCandidates(ref Board board)
    {
        Candidates = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        int[] segmentIDs = GetSegments();
        List<int> candidatesList = new();
        Segment[] segments = new Segment[] { board.Segments[segmentIDs[0]], board.Segments[segmentIDs[1]], board.Segments[segmentIDs[2]] };
        foreach (Segment sgmnt in segments)
        {
            foreach (int i in Candidates)
            {
                if (!sgmnt.ValidValues.Contains(i)) candidatesList.Add(i);
            }
        }
        foreach (int i in candidatesList)
        {
            Candidates.Remove(i);
        }
        return Candidates;
    }
}