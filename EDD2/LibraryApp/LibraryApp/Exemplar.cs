using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    // Represents a physical copy (exemplar) of a book.
    public class Exemplar
    {
        private readonly int tombo;
        private readonly List<Emprestimo> emprestimos;

        public int Tombo => tombo;

        public Exemplar(int tomboInicial)
        {
            tombo = tomboInicial;
            emprestimos = new List<Emprestimo>();
        }

        // Attempt to loan this exemplar. Returns true if success.
        public bool Emprestar()
        {
            if (!Disponivel())
                return false;

            var novoEmprestimo = new Emprestimo(DateTime.Now);
            emprestimos.Add(novoEmprestimo);
            return true;
        }

        // Attempt to return this exemplar. Returns true if a pending loan was closed.
        public bool Devolver()
        {
            var emprestimoAberto = emprestimos.LastOrDefault(e => e.DtDevolucao == null);
            if (emprestimoAberto == null)
                return false;

            emprestimoAberto.DtDevolucao = DateTime.Now;
            return true;
        }

        // Is this exemplar currently available?
        public bool Disponivel()
        {
            return !emprestimos.Any(e => e.DtDevolucao == null);
        }

        // Number of loans this exemplar had in total.
        public int QtdeEmprestimos()
        {
            return emprestimos.Count;
        }

        // Returns a read-only view of loans for reporting.
        public IReadOnlyList<Emprestimo> GetEmprestimos()
        {
            return emprestimos.AsReadOnly();
        }
    }
}
