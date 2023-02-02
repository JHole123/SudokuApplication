using System;

namespace SudokuApplication;

partial class SudokuForm
{
        /// <summary>
        ///  Required designer variable.
        /// </summary>
    private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    // i EDITED IT MWAHAHAH
    private void InitializeComponent()
    {

        this.Name = "SudokuForm";
        this.Text = "Sudoku";
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1264, 681);
        this.BackColor = Color.DarkSlateGray;
        this.SuspendLayout();

        #region Placing Sudoku Board
        Point sudokuBoardLocation = new Point(50, 50);
        int sudokuTileMargin = 5, X, Y;
        TextBox tb;
        for (int y = 1; y < 10; y++)
        {
            for (int x = 1; x < 10; x++)
            {
                tb = new();
                tb.Cursor = System.Windows.Forms.Cursors.Hand;
                tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                tb.Size = new System.Drawing.Size(50, 50);
                X = sudokuBoardLocation.X + ((x - 1) * (tb.Size.Width + sudokuTileMargin));
                Y = sudokuBoardLocation.Y + ((y - 1) * (50 + sudokuTileMargin)); // height is hardcoded tb.Size.Height doesn't work
                if (x > 3) X += sudokuTileMargin;
                if (x > 6) X += sudokuTileMargin;
                if (y > 3) Y += sudokuTileMargin;
                if (y > 6) Y += sudokuTileMargin;
                tb.Location = new System.Drawing.Point(X, Y); 
                tb.MaximumSize = new System.Drawing.Size(50, 50);
                tb.Multiline = true;
                tb.Name = $"Tile{y}{x}";
                tb.TabIndex = 16;
                tb.TabStop = false;
                tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                tb.WordWrap = false;
                tb.Click += new System.EventHandler(this.TileClicked);
                tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TilePressed);
                tb.Leave += new System.EventHandler(this.TileUnclicked);
                this.Controls.Add(tb);

            }
        }
        #endregion

        /*#region Placing Drag & Drop Label
        Point dragAndDropLabelLocation = new Point(800, 100);
        Label l = new();
        l.Text = "Drag and drop your *.txt files here";
        l.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        l.Location = dragAndDropLabelLocation;
        l.Size = new System.Drawing.Size(500, 50);
        l.AllowDrop = true;
        l.DragEnter += new System.Windows.Forms.DragEventHandler(this.BoardDragEnter);
        l.DragDrop += new System.Windows.Forms.DragEventHandler(this.BoardDragDrop);
        this.Controls.Add(l);
        #endregion*/

        #region Placing Picture Box
        Point dragAndDropIconLocation = new Point(600, 50);
        PictureBox pb = new();
        pb.Location = dragAndDropIconLocation;
        //pb.Size = new Size(250, 250);
        pb.SizeMode = PictureBoxSizeMode.AutoSize;
        pb.ImageLocation = @"M:\Pogramming\SudokuApplication\SudokuApplication\dragdropicon.png";
        pb.AllowDrop = true;
        pb.DragEnter += new System.Windows.Forms.DragEventHandler(this.BoardDragEnter);
        pb.DragDrop += new System.Windows.Forms.DragEventHandler(this.BoardDragDrop);
        this.Controls.Add(pb);
        #endregion

        #region Placing Hint Buttons
        Point buttonLocation = new Point(800, 150);
        Button b = new Button();
        b.Name = "ShowHintButton";
        b.FlatStyle = FlatStyle.Flat;
        b.Size = new Size(150, 75);
        b.Location = buttonLocation;
        b.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        b.Text = "Show hint";
        b.Click += new EventHandler(this.GenerateHint);
        this.Controls.Add(b);

        buttonLocation = new(960, 150);
        b = new();
        b.Name = "HideHintButton";
        b.FlatStyle = FlatStyle.Flat;
        b.Size = new Size(150, 75);
        b.Location = buttonLocation;
        b.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        b.Text = "Hide hint";
        b.Click += new EventHandler(this.HideHint);
        this.Controls.Add(b);
        #endregion

        #region Placing Hint Title
        Point hintTitleLabelLocation = new Point(595, 320);
        Label t = new Label();
        t.Name = "HintTitleLabel";
        t.Location = hintTitleLabelLocation;
        t.MaximumSize = new Size(600, 0);
        t.AutoSize = true;
        t.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        t.Text = "Hint Title";
        this.Controls.Add(t);
        #endregion

        #region Placing Hint Tally Label
        Point hintTallyLabelLocation = new Point(800, 80);
        Label c = new Label();
        c.Name = "HintTallyLabel";
        c.Location = hintTallyLabelLocation;
        c.MaximumSize = new Size(600, 0);
        c.AutoSize = true;
        c.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        c.Text = "Hints generated: 0";
        this.Controls.Add(c);
        #endregion

        #region Placing Hint Label
        Point hintLabelLocation = new Point(600, 380);
        Label l = new Label();
        l.Name = "HintLabel";
        l.Location = hintLabelLocation;
        l.MaximumSize = new Size(600, 0);
        l.AutoSize = true;
        l.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        l.Text = "This is some example text of a hint if you do it long enough does it go off the screen or does it wrap";
        this.Controls.Add(l);
        #endregion

        this.ResumeLayout(false);
        this.PerformLayout();
    }


    }
#endregion