namespace SameFiles.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null!;
        private System.Windows.Forms.TextBox txtFolder = null!;
        private System.Windows.Forms.Button btnBrowse = null!;
        private System.Windows.Forms.Button btnScan = null!;
        private System.Windows.Forms.Button btnDelete = null!;
        private System.Windows.Forms.ProgressBar progressBar = null!;
        private System.Windows.Forms.Label lblProgress = null!;
        private System.Windows.Forms.DataGridView grid = null!;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDelete = null!;
        private System.Windows.Forms.DataGridViewImageColumn colPreview = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath = null!;
        private System.Windows.Forms.DataGridViewButtonColumn colOpen = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtFolder = new System.Windows.Forms.TextBox();
            btnBrowse = new System.Windows.Forms.Button();
            btnScan = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            lblProgress = new System.Windows.Forms.Label();
            grid = new System.Windows.Forms.DataGridView();
            colDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colPreview = new System.Windows.Forms.DataGridViewImageColumn();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colOpen = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // txtFolder
            txtFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtFolder.Location = new System.Drawing.Point(12, 12);
            txtFolder.Size = new System.Drawing.Size(400, 23);
            // btnBrowse
            btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnBrowse.Location = new System.Drawing.Point(418, 11);
            btnBrowse.Size = new System.Drawing.Size(32, 23);
            btnBrowse.Text = "...";
            btnBrowse.Click += btnBrowse_Click;
            // btnScan
            btnScan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnScan.Location = new System.Drawing.Point(456, 11);
            btnScan.Size = new System.Drawing.Size(75, 23);
            btnScan.Text = "Scan";
            btnScan.Click += btnScan_Click;
            // btnDelete
            btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnDelete.Location = new System.Drawing.Point(537, 11);
            btnDelete.Size = new System.Drawing.Size(113, 23);
            btnDelete.Text = "Delete selected";
            btnDelete.Enabled = false;
            btnDelete.Click += btnDelete_Click;
            // progressBar
            progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            progressBar.Location = new System.Drawing.Point(12, 40);
            progressBar.Size = new System.Drawing.Size(638, 23);
            // lblProgress
            lblProgress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            lblProgress.Location = new System.Drawing.Point(12, 66);
            lblProgress.Size = new System.Drawing.Size(200, 15);
            lblProgress.Text = "0/0";
            // grid
            grid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grid.Location = new System.Drawing.Point(12, 84);
            grid.Size = new System.Drawing.Size(638, 354);
            grid.AllowUserToAddRows = false;
            grid.AutoGenerateColumns = false;
            grid.RowTemplate.Height = 96;
            grid.CellContentClick += grid_CellContentClick;
            grid.CellValueChanged += grid_CellValueChanged;
            grid.CurrentCellDirtyStateChanged += grid_CurrentCellDirtyStateChanged;
            grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colDelete, colPreview, colName, colSize, colPath, colOpen });
            // colDelete
            colDelete.DataPropertyName = "Delete";
            colDelete.HeaderText = "Delete";
            colDelete.Width = 50;
            // colPreview
            colPreview.DataPropertyName = "Preview";
            colPreview.HeaderText = "Preview";
            colPreview.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            colPreview.Width = 100;
            // colName
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Name";
            colName.Width = 150;
            // colSize
            colSize.DataPropertyName = "Size";
            colSize.HeaderText = "Size";
            colSize.Width = 80;
            // colPath
            colPath.DataPropertyName = "Directory";
            colPath.HeaderText = "Path";
            colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // colOpen
            colOpen.HeaderText = "Open";
            colOpen.Text = "Open";
            colOpen.UseColumnTextForButtonValue = true;
            colOpen.Width = 60;
            // MainForm
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(662, 450);
            Controls.Add(grid);
            Controls.Add(lblProgress);
            Controls.Add(progressBar);
            Controls.Add(btnDelete);
            Controls.Add(btnScan);
            Controls.Add(btnBrowse);
            Controls.Add(txtFolder);
            Text = "SameFiles";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
