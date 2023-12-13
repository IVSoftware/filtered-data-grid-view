namespace filtered_data_grid_view
{
    partial class MainForm
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
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            dataGridView = new DataGridView();
            checkBoxFilterChampions = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1539F));
            tableLayoutPanel1.Controls.Add(dataGridView, 0, 1);
            tableLayoutPanel1.Controls.Add(checkBoxFilterChampions, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(5, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 395F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1740, 440);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.FromArgb(0, 0, 192);
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dataGridView, 2);
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 48);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 62;
            dataGridView.Size = new Size(1734, 389);
            dataGridView.TabIndex = 1;
            // 
            // checkBoxFilterChampions
            // 
            checkBoxFilterChampions.Appearance = Appearance.Button;
            checkBoxFilterChampions.BackColor = Color.FromArgb(0, 0, 192);
            checkBoxFilterChampions.Dock = DockStyle.Fill;
            checkBoxFilterChampions.ForeColor = Color.White;
            checkBoxFilterChampions.Location = new Point(3, 3);
            checkBoxFilterChampions.Name = "checkBoxFilterChampions";
            checkBoxFilterChampions.Size = new Size(195, 39);
            checkBoxFilterChampions.TabIndex = 2;
            checkBoxFilterChampions.Text = "FilterChampions";
            checkBoxFilterChampions.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxFilterChampions.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1750, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Padding = new Padding(5);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Roster";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView;
        private CheckBox checkBoxFilterChampions;
    }
}