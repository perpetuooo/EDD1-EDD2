// Pedro Perpétuo CB3021688
// Igor Benites CB3021734

using System;
using System.Windows.Forms;
namespace EscolaWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
