using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BilheteriaApp
{
    public partial class BilheteriaForm : Form
    {
        private const int FIL = 15;
        private const int POL = 40;
        private bool[,] mat = new bool[FIL, POL];
        private Panel panel;
        private Label faturamentoLabel;

        public BilheteriaForm()
        {
            InitComponents();
            InitMatrix();
            UpdateGrid();
            CreateControls();
        }

        private void InitMatrix()
        {
            for (int i = 0; i < FIL; i++)
                for (int j = 0; j < POL; j++)
                    mat[i, j] = false;
        }

        private void Randomize()
        {
            var rnd = new Random();
            for (int i = 0; i < FIL; i++)
            {
                for (int j = 0; j < POL; j++)
                    mat[i, j] = false;
                for (int k = 0; k < 10; k++)
                {
                    int r = rnd.Next(POL);
                    mat[i, r] = true;
                }
            }
        }

        private void UpdateGrid()
        {
            for (int r = 0; r < FIL; r++)
            {
                for (int c = 0; c < POL; c++)
                {
                    var cell = poltronas.Rows[r].Cells[c];
                    if (mat[r, c])
                    {
                        cell.Value = "!";
                        cell.Style.BackColor = Color.IndianRed;
                        cell.Style.ForeColor = Color.White;
                    }
                    else
                    {
                        cell.Value = "O";
                        cell.Style.BackColor = Color.GreenYellow;
                        cell.Style.ForeColor = Color.Black;
                    }
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Randomize_Click(object sender, EventArgs e)
        {
            Randomize();
            UpdateGrid();
        }

        private void Reserve_Click(object sender, EventArgs e)
        {
            ReservePoltronaControls();
        }

        private void ReservePoltronaControls()
        {
            RemovePanel();
            panel = new Panel();
            panel.Height = 90;
            panel.Dock = DockStyle.Bottom;
            panel.Padding = new Padding(10);

            var lblFil = new Label { Text = "Fileira (1-15):", AutoSize = true, Location = new Point(10, 10), Font = new Font("Consolas", 9F, FontStyle.Regular) };
            var nudFil = new NumericUpDown { Minimum = 1, Maximum = FIL, Location = new Point(110, 8) };
            var lblPol = new Label { Text = "Poltrona (1-40):", AutoSize = true, Location = new Point(10, 40), Font = new Font("Consolas", 9F, FontStyle.Regular) };
            var nudPol = new NumericUpDown { Minimum = 1, Maximum = POL, Location = new Point(110, 38) };
            var btnOk = new Button { Text = "Confirmar", AutoSize = true, Location = new Point(220, 8), Font = new Font("Consolas", 9F, FontStyle.Regular) };
            var btnCancel = new Button { Text = "Cancelar", AutoSize = true, Location = new Point(220, 38), Font = new Font("Consolas", 9F, FontStyle.Regular) };

            btnOk.Click += (s, e) =>
            {
                int fil = (int)nudFil.Value;
                int pol = (int)nudPol.Value;
                if (mat[fil - 1, pol - 1])
                {
                    MessageBox.Show("Poltrona ocupada. Tente outra.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mat[fil - 1, pol - 1] = true;
                UpdateGrid();
                RemovePanel();
                MessageBox.Show("Poltrona reservada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnCancel.Click += (s, e) => RemovePanel();

            panel.Controls.Add(lblFil);
            panel.Controls.Add(nudFil);
            panel.Controls.Add(lblPol);
            panel.Controls.Add(nudPol);
            panel.Controls.Add(btnOk);
            panel.Controls.Add(btnCancel);

            this.Controls.Add(panel);
            panel.BringToFront();
        }

        private void RemovePanel()
        {
            if (panel != null)
            {
                this.Controls.Remove(panel);
                panel.Dispose();
                panel = null;
            }
        }

        private void PoltronasDC(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (mat[r, c])
            {
                MessageBox.Show($"Poltrona {r + 1}x{c + 1} já está ocupada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var dr = MessageBox.Show($"Deseja reservar a poltrona {r + 1} x {c + 1}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                mat[r, c] = true;
                UpdateGrid();
                MessageBox.Show("Poltrona reservada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int Faturamento()
        {
            int qtd = 0;
            int fat = 0;
            for (int i = 0; i < FIL; i++)
            {
                for (int j = 0; j < POL; j++)
                {
                    if (mat[i, j])
                    {
                        qtd++;
                        if (i < 5) fat += 50;
                        else if (i < 10) fat += 30;
                        else fat += 15;
                    }
                }
            }
            return fat;
        }

        private void CreateControls()
        {
            var flow = this.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (flow == null) return;
            var btn = new Button { Text = "Faturamento", AutoSize = true, Font = new Font("Consolas", 9F, FontStyle.Regular) };
            faturamentoLabel = new Label { Text = "", AutoSize = true, Padding = new Padding(10, 6, 10, 6), Font = new Font("Consolas", 9F, FontStyle.Regular) };
            btn.Click += (s, e) => faturamentoLabel.Text = "Valor da bilheteria: R$" + Faturamento();
            flow.Controls.Add(btn);
            flow.Controls.Add(faturamentoLabel);
        }
    }
}
