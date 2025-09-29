using System;
using System.Linq;
using System.Windows.Forms;

namespace AgendaWinForms
{
    public class MainForm : Form
    {
        private TextBox txtNome = new TextBox { PlaceholderText = "Nome" };
        private TextBox txtEmail = new TextBox { PlaceholderText = "Email" };
        private TextBox txtDia = new TextBox { PlaceholderText = "Dia", Width = 40 };
        private TextBox txtMes = new TextBox { PlaceholderText = "Mês", Width = 40 };
        private TextBox txtAno = new TextBox { PlaceholderText = "Ano", Width = 60 };
        private Button btnAdicionar = new Button { Text = "Adicionar" };
        private Button btnRemover = new Button { Text = "Remover Selecionado" };
        private ListBox lstContatos = new ListBox();
        private Contatos contatos = new Contatos();

        public MainForm()
        {
            Text = "Agenda";
            Width = 400;
            Height = 350;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblNome = new Label { Text = "Nome:", Top = 10, Left = 10, Width = 50 };
            txtNome.Top = 10; txtNome.Left = 70; txtNome.Width = 300;
            var lblEmail = new Label { Text = "Email:", Top = 40, Left = 10, Width = 50 };
            txtEmail.Top = 40; txtEmail.Left = 70; txtEmail.Width = 300;
            var lblData = new Label { Text = "Nascimento:", Top = 70, Left = 10, Width = 70 };
            txtDia.Top = 70; txtDia.Left = 90;
            txtMes.Top = 70; txtMes.Left = 140;
            txtAno.Top = 70; txtAno.Left = 190;

            btnAdicionar.Top = 100; btnAdicionar.Left = 10; btnAdicionar.Width = 120;
            btnRemover.Top = 100; btnRemover.Left = 140; btnRemover.Width = 150;

            lstContatos.Top = 140; lstContatos.Left = 10; lstContatos.Width = 360; lstContatos.Height = 150;

            Controls.AddRange(new Control[] { lblNome, txtNome, lblEmail, txtEmail, lblData, txtDia, txtMes, txtAno, btnAdicionar, btnRemover, lstContatos });

            btnAdicionar.Click += BtnAdicionar_Click;
            btnRemover.Click += BtnRemover_Click;
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDia.Text, out int dia) || !int.TryParse(txtMes.Text, out int mes) || !int.TryParse(txtAno.Text, out int ano))
            {
                MessageBox.Show("Data de nascimento inválida.");
                return;
            }
            var contato = new Contato(txtNome.Text, txtEmail.Text, new Data(dia, mes, ano));
            if (contatos.Adicionar(contato))
            {
                AtualizarLista();
                txtNome.Text = txtEmail.Text = txtDia.Text = txtMes.Text = txtAno.Text = "";
            }
            else
            {
                MessageBox.Show("Já existe um contato com esse email.");
            }
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (lstContatos.SelectedItem is Contato contato)
            {
                contatos.Remover(contato);
                AtualizarLista();
            }
        }

        private void AtualizarLista()
        {
            lstContatos.DataSource = null;
            lstContatos.DataSource = contatos.Agenda.ToList();
            lstContatos.DisplayMember = "Nome";
        }
    }
}
