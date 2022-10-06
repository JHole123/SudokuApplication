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
                for (int j = 0; j < 9; j++)
                {
                    if (candidates.Contains(j + 1))
                    {
                        if (j == 8) result += $"{candidates[j]}";
                        else if (j % 3 == 0) result += $"{candidates[j]}\n";
                        else result += $"{candidates[j]} ";
                    }
                    else
                    {
                        if (j == 8) result += $" ";
                        else if (j % 3 == 2) result += $" \n";
                        else result += $"  ";
                    }
                }
                GraphicalTiles[i].Text = result;
                result = "";
            }
        }
    }
}