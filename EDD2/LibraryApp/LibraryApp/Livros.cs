using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    public class Livros
    {
        private readonly List<Livro> acervo;

        public Livros()
        {
            acervo = new List<Livro>();
        }

''
        public void Adicionar(Livro livro)
        {
            acervo.Add(livro);
        }

        public Livro? PesquisarPorIsbn(int isbn)
        {
            return acervo.FirstOrDefault(l => l.ISBN == isbn);
        }

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
