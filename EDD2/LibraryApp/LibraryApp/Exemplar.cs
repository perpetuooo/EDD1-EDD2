using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    public class Exemplar
    
        private readonly int tombo;
        private readonly List<Emprestimo> emprestimos;

        public int Tombo => tombo;

        public Exemplar(int tomboInicial)
        {
            tombo = tomboInicial;
            emprestimos = new List<Emprestimo>();
        }

        public bool Emprestar()
        {
            if (!Disponivel())
                return false;

            var novoEmprestimo = new Emprestimo(DateTime.Now);
            emprestimos.Add(novoEmprestimo);
            return true;
        }

        public bool Devolver()
        {
            var emprestimoAberto = emprestimos.LastOrDefault(e => e.DtDevolucao == null);
            if (emprestimoAberto == null)
                return false;

            emprestimoAberto.DtDevolucao = DateTime.Now;
            return true;
        }

        public bool Disponivel()
        {
            return !emprestimos.Any(e => e.DtDevolucao == null);
        }

        public int QtdeEmprestimos()
        {
            return emprestimos.Count;
        }

        public IReadOnlyList<Emprestimo> GetEmprestimos()
        {
            return emprestimos.AsReadOnly();
        }
    
}
