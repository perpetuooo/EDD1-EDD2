using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
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

        public void AdicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int QtdeExemplares()
        {
            return exemplares.Count;
        }

        public int QtdeDisponiveis()
        {
            return exemplares.Count(e => e.Disponivel());
        }

        public int QtdeEmprestimos()
        {
            return exemplares.Sum(e => e.QtdeEmprestimos());
        }

        public double PercDisponibilidade()
        {
            int total = QtdeExemplares();
            if (total == 0) return 0.0;
            return (QtdeDisponiveis() * 100.0) / total;
        }

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
