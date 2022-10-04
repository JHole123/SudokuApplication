using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApplication;

public class Board 
{
    public List<Tile> Tiles { get; set; }
    public Tile this[int n]
    {
        get { return Tiles[n]; }
        set { Tiles[n] = value; }
    }
}

