namespace SudokuEngine;

public class BoardTemplate
{
    string alphabet = "ABCDEFGHI";
    public List<string> Tiles = new();
    public BoardTemplate(Board board)
    {
        foreach (Tile t in board.Tiles)
        {
            Tiles.Add(alphabet[t.Value - 1].ToString()); // converts each number to a letter
        }
    }

    public void SaveTemplate()
    {

    }
}
