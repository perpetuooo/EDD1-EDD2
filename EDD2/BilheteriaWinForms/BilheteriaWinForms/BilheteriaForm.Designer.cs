using System;
using System.Windows.Forms;
using System.Drawing;

namespace BilheteriaApp
{
    public partial class BilheteriaForm : Form
    {
        private DataGridView poltronas;
        private Button btnRandomize;
        private Button btnReserve;

        private void InitComponents()
        {
            this.Text = "Projeto Bilheteria";
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(1100, 700);

            poltronas = new DataGridView();
            poltronas.AllowUserToAddRows = false;
            poltronas.RowHeadersWidth = 60;
            poltronas.ReadOnly = true;
            poltronas.SelectionMode = DataGridViewSelectionMode.CellSelect;
            poltronas.MultiSelect = false;
            poltronas.Dock = DockStyle.Top;
            poltronas.Height = 480;
            poltronas.Font = new Font("Consolas", 10F);
            poltronas.CellDoubleClick += new DataGridViewCellEventHandler(PoltronasDC);

            for (int c = 0; c < 40; c++)
            {
                var col = new DataGridViewTextBoxColumn();
                col.Name = (c + 1).ToString();
                col.HeaderText = (c + 1).ToString();
                col.Width = 28;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                poltronas.Columns.Add(col);
            }

            poltronas.Rows.Clear();
            for (int r = 0; r < 15; r++)
            {
                int idx = poltronas.Rows.Add();
                poltronas.Rows[idx].HeaderCell.Value = (r + 1).ToString();
            }

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Bottom;
            panel.Height = 120;
            panel.Padding = new Padding(10);
            panel.FlowDirection = FlowDirection.LeftToRight;

            btnRandomize = new Button();
            btnRandomize.Text = "Randomizar";
            btnRandomize.AutoSize = true;
            btnRandomize.Font = new Font("Consolas", 9F, FontStyle.Regular);
            btnRandomize.Padding = new Padding(6);
            btnRandomize.Click += new EventHandler(Randomize_Click);

            btnReserve = new Button();
            btnReserve.Text = "Reservar poltrona";
            btnReserve.AutoSize = true;
            btnReserve.Font = new Font("Consolas", 9F, FontStyle.Regular);
            btnReserve.Padding = new Padding(6);
            btnReserve.Click += new EventHandler(Reserve_Click);

            panel.Controls.Add(btnRandomize);
            panel.Controls.Add(btnReserve);

            this.Controls.Add(poltronas);
            this.Controls.Add(panel);
        }
    }
}
