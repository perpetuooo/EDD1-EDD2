using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    // Represents a book with a collection of exemplars.
    public class Livro
    {
        private readonly int isbn;
        private readonly string titulo;
        private readonly string autor;
        private readonly string editora;
        private readonly List<Exemplar> exemplares;

        public int ISBN => isbn;
        public string Titulo => titulo;
        public string Autor => autor;
        public string Editora => editora;

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            this.exemplares = new List<Exemplar>();
        }

        // Adds an exemplar to this book. The exemplar tombo should be unique (handled by caller).
        public void AdicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        // Total number of exemplares for this title.
        public int QtdeExemplares()
        {
            return exemplares.Count;
        }

        // Quantity of exemplares currently available.
        public int QtdeDisponiveis()
        {
            return exemplares.Count(e => e.Disponivel());
        }

        // Total number of emprestimos across all exemplares.
        public int QtdeEmprestimos()
        {
            return exemplares.Sum(e => e.QtdeEmprestimos());
        }

        // Percentage availability: (available / total) * 100
        public double PercDisponibilidade()
        {
            int total = QtdeExemplares();
            if (total == 0) return 0.0;
            return (QtdeDisponiveis() * 100.0) / total;
        }

        // Return a read-only list of exemplares for reporting.
        public IReadOnlyList<Exemplar> GetExemplares()
        {
            return exemplares.AsReadOnly();
        }

        public override string ToString()
        {
            return $"ISBN: {isbn} | Título: {titulo} | Autor: {autor} | Editora: {editora}";
        }
    }
}
