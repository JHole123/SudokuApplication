namespace SudokuEngine;

public class Board
{
    public List<Tile> Tiles = new();
    public List<Segment> Segments = new();
    public Tile this[int n]
    {
        get { return Tiles[n]; }
        set { Tiles[n] = value; }
    }
}
