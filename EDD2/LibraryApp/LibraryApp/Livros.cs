using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    // Container for book collection.
    public class Livros
    {
        private readonly List<Livro> acervo;

        public Livros()
        {
            acervo = new List<Livro>();
        }

        // Adds a book to the collection.
        public void Adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        // Searches book by ISBN. Returns null if not found.
        public Livro? PesquisarPorIsbn(int isbn)
        {
            return acervo.FirstOrDefault(l => l.ISBN == isbn);
        }

        // Searches books by title (contains, case-insensitive).
        public IEnumerable<Livro> PesquisarPorTitulo(string tituloParcial)
        {
            return acervo.Where(l => l.Titulo.IndexOf(tituloParcial, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public IReadOnlyList<Livro> GetTodos()
        {
            return acervo.AsReadOnly();
        }
    }
}
