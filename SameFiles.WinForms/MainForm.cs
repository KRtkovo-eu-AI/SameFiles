using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SameFiles.WinForms
{
    public partial class MainForm : Form
    {
        private readonly BindingList<DuplicateFile> _duplicates = new();

        public MainForm()
        {
            InitializeComponent();
            grid.DataSource = _duplicates;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
                txtFolder.Text = dlg.SelectedPath;
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            btnScan.Enabled = false;
            _duplicates.Clear();
            var files = await DuplicateFinder.FindDuplicatesAsync(txtFolder.Text);
            foreach (var file in files)
                _duplicates.Add(file);
            btnScan.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var groups = _duplicates.GroupBy(f => f.Hash);
            var toRemove = new List<DuplicateFile>();
            foreach (var group in groups)
            {
                var marked = group.Where(f => f.Delete).ToList();
                if (marked.Count == 0) continue;
                if (marked.Count >= group.Count()) continue;

                foreach (var file in marked)
                {
                    try
                    {
                        File.Delete(file.Path);
                        toRemove.Add(file);
                    }
                    catch (IOException)
                    {
                        // ignore errors
                    }
                }
            }
            foreach (var f in toRemove)
                _duplicates.Remove(f);
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (grid.Columns[e.ColumnIndex] == colOpen)
            {
                var file = (DuplicateFile)grid.Rows[e.RowIndex].DataBoundItem;
                try
                {
                    Process.Start("explorer.exe", $"/select,\"{file.Path}\"");
                }
                catch (Exception)
                {
                }
            }
        }

        private void grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grid.IsCurrentCellDirty)
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (grid.Columns[e.ColumnIndex] == colDelete)
            {
                var file = (DuplicateFile)grid.Rows[e.RowIndex].DataBoundItem;
                var group = _duplicates.Where(f => f.Hash == file.Hash).ToList();
                var checkedCount = group.Count(f => f.Delete);
                if (checkedCount >= group.Count)
                {
                    file.Delete = false;
                    grid.Refresh();
                }
            }
        }
    }
}
