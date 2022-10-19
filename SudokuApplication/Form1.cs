namespace SudokuApplication
{
    public partial class SudokuForm : Form
    {
        public List<TextBox> GraphicalTiles;
        public Board MainBoard = new Board();
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
            string data = string.Join("", File.ReadAllLines((e.Data.GetData(DataFormats.FileDrop) as string[])[0]));
            if (!data.Any(x => char.IsLetter(x)) && data.Length == 81) {
                for (int i = 0; i < 81; i++)
                {
                    GraphicalTiles[i].Text = data[i] == '0' ? " " : data[i].ToString();
                }
            }
        }

        private void BoardDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void GenerateGraphicalCandidates()
        {
            string result = "";
            for (int i = 0; i < 81; i++)
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
                GraphicalTiles[i].Text = result;
                result = "";
            }
        }
    }
}