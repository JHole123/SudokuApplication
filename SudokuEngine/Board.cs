﻿namespace SudokuEngine;

public class Board
{
    public List<Tile> Tiles = new();
    public List<Segment> Segments = new();
    public Board()
    {
        for (int i = 0; i < 81; i++) Tiles.Add(new(i));
        PopulateSegmentTileIDs();

    }
    public Tile this[int n]
    {
        get { return Tiles[n]; }
        set { Tiles[n] = value; }
    }
    public void UpdateSegmentValidValues()
    {
        foreach (Segment s in Segments)
        {
            s.UpdateValidValues(Tiles);
        }
    }
    private void PopulateSegmentTileIDs()
    {
        Segment sgmnt= new();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++) sgmnt.Tiles.Add((i * 9) + j); // Horizontal segments beginning at top
            Segments.Add(sgmnt);
            sgmnt = new();

            for (int j = 0; j < 9; j++) sgmnt.Tiles.Add((j * 9) + i); // Vertical segments beginning at left
            Segments.Add(sgmnt);
            sgmnt = new();
        }

        // the 3x3 segments...
        for (int bigY = 0; bigY < 8; bigY += 3) {
            for (int bigX = 0; bigX < 8; bigX += 3) {
                for (int y = bigY; y < bigY + 3; y++) {
                    for (int x = bigX; x < bigX + 3; x++) sgmnt.Tiles.Add((y * 9) + x);
                }
                Segments.Add(sgmnt);
                sgmnt = new();
            }
        }
    }
}
