using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApplication;

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

