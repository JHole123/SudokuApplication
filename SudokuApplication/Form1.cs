namespace SudokuApplication
{
    public partial class SudokuForm : Form
    {
        public List<TextBox> GraphicalTiles;
        public SudokuForm()
        {
            InitializeComponent();
            GraphicalTiles = new List<TextBox>(this.Controls.OfType<TextBox>()
                .Where(i => String.IsNullOrEmpty(i.Text))
                .OrderBy(i => i.Name)
                .ToArray());
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

        }
    }
}