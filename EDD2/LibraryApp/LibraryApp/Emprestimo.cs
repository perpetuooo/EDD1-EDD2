using System;

namespace BibliotecaApp
{
    // Represents a loan (emprestimo) for an exemplar.
    public class Emprestimo
    {
        public DateTime DtEmprestimo { get; set; }
        public DateTime? DtDevolucao { get; set; } // null means not returned yet

        public Emprestimo(DateTime dtEmprestimo)
        {
            DtEmprestimo = dtEmprestimo;
            DtDevolucao = null;
        }
    }
}
