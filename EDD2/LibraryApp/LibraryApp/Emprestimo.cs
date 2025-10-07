using System;

namespace BibliotecaApp
{
    
    public class Emprestimo
    {
        public DateTime DtEmprestimo { get; set; }
        public DateTime? DtDevolucao { get; set; }
        public Emprestimo(DateTime dtEmprestimo)
        {
            DtEmprestimo = dtEmprestimo;
            DtDevolucao = null;
        }
    }
}
