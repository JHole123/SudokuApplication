using SudokuEngine;

namespace SudokuApplication;

public partial class SudokuForm : Form
{
    public List<TextBox> GraphicalTiles;
    public Board MainBoard = new();
    public SudokuForm()
    {
        InitializeComponent();
        GraphicalTiles = new List<TextBox>(Controls.OfType<TextBox>()
            .Where(i => string.IsNullOrEmpty(i.Text))
            .OrderBy(i => i.Name)
            .ToArray());
        for (int i = 0; i < GraphicalTiles.Count; i++)
        {
            MainBoard.Tiles.Add(new Tile(i));
        }
        for (int i = 0; i < 27; i++)
        {
            MainBoard.Segments.Add(new Segment());
        }
        GenerateGraphicalCandidates();
    }

    private void BoardDragDrop(object sender, DragEventArgs e)
    {
        string data = string.Join("", File.ReadAllLines(((string[])e.Data!.GetData(DataFormats.FileDrop))[0]));
        if (!data.Any(x => char.IsLetter(x) && char.IsSymbol(x)) && data.Length == 81) // check the board is all numbers and 81 long
        {
            for (int j = 0; j < 81; j++)
            {
                MainBoard.Tiles[j].Value = data[j] - 48; // why subtract 48? i have no clue but it breaks if i don't
            }
            //for (int i = 0; i < 81; i++)
            //{
            //    GraphicalTiles[i].Text = data[i] == '0' ? " " : data[i].ToString(); // replace 0s with empty tiles
            //}
        }
        GenerateGraphicalCandidates();
    }

    private void BoardDragEnter(object sender, DragEventArgs e)
    {
        if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
        else
            e.Effect = DragDropEffects.None;
    }

    private void GenerateGraphicalCandidates()
    {
        string result = "";
        for (int i = 0; i < 81; i++)
        {
            if (MainBoard.Tiles[i].Value != 0)
            {
                result = MainBoard.Tiles[i].Value.ToString();
                GraphicalTiles[i].Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            }
            else
            {
                int[] candidates = MainBoard.Tiles[i].GetCandidates(ref MainBoard);
                // this was a lot harder to do than it should have been, never forget 
                for (int j = 1; j < 10; j++) // i'm so sorry
                {
                    if (candidates.Contains(j))
                    {
                        if (j == 9) result += $"{j}";
                        else if (j % 3 == 0) result += $"{j}\r\n";
                        else result += $"{j} ";
                    }
                    else
                    {
                        if (j == 9) result += $" ";
                        else if (j % 3 == 0) result += $" \r\n";
                        else result += $"  ";
                    }
                }
            }
            GraphicalTiles[i].Text = result;
            result = "";
        }
    }

    private void TileClicked(object sender, EventArgs e)
    {
        var obj = (sender as TextBox)!;
        if (obj.Text.Length > 1)
        {
            obj.Text = "";
            obj.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
        }
    }

    private void TileUnclicked(object sender, EventArgs e)
    {
        var obj = (sender as TextBox)!;
        if (obj.Text == "")
        {
            obj.Font = new Font("Microsoft Sans Serif", 5.25F, FontStyle.Bold, GraphicsUnit.Point);
            GenerateGraphicalCandidates();
        }
    }

    private void TilePressed(object sender, KeyPressEventArgs e)
    {
        // for some reason, e.Handled is the wrong way around
        // this caused me much grief
        var obj = (sender as TextBox)!;
        e.Handled = !((obj.Text.Length == 0 && "123456789".Contains(e.KeyChar)) || 
                      (obj.Text.Length == 1 && e.KeyChar == (char)Keys.Back));
    }
}
