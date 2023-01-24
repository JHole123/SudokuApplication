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
        this.SuspendLayout();

        #region Placing Sudoku Board
        Point sudokuBoardLocation = new Point(100, 100);
        int sudokuTileMargin = 5;
        TextBox tb;
        for (int y = 1; y < 10; y++)
        {
            for (int x = 1; x < 10; x++)
            {
                tb = new();
                tb.Cursor = System.Windows.Forms.Cursors.Hand;
                tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                tb.Size = new System.Drawing.Size(33, 33);
                tb.Location = new System.Drawing.Point(sudokuBoardLocation.X + ((x - 1) * (tb.Size.Width + sudokuTileMargin)), sudokuBoardLocation.Y + 2 * ((y-1) * (tb.Size.Height + sudokuTileMargin))); // really don't know why a 2* works here, but it does space it out properly
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

        #region Placing Drag & Drop Label
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
        #endregion

        this.ResumeLayout(false);
        this.PerformLayout();
    }


    }
#endregion