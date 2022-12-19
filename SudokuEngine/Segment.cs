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
    // list of references to tiles within this segment
}

