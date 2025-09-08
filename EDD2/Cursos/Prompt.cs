using System.Windows.Forms;
namespace EscolaWinForms
{
    public static class Prompt
    {
        public static string ShowDialog(string texto, string titulo)
        {
            var prompt = new Form { Width = 520, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, Text = titulo, StartPosition = FormStartPosition.CenterScreen };
            var textLabel = new Label { Left = 10, Top = 10, Text = texto, Width = 480 };
            var textBox = new TextBox { Left = 10, Top = 36, Width = 480 };
            var confirmation = new Button { Text = "Ok", Left = 320, Width = 80, Top = 68, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancelar", Left = 410, Width = 80, Top = 68, DialogResult = DialogResult.Cancel };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }
    }
}
