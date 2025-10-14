using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_de_Projetos.Models
{
    public class Projetos
    {
        private int nextProjetoId = 1;
        public List<Projeto> itens { get; set; }

        public Projetos()
        {
            itens = new List<Projeto>();
        }

        public bool adicionar(Projeto p)
        {
            p.id = nextProjetoId++;
            itens.Add(p);
            return true;
        }

        public bool remover(Projeto p)
        {
            return itens.RemoveAll(x => x.id == p.id) > 0;
        }

        public Projeto? buscar(int id)
        {
            return itens.FirstOrDefault(x => x.id == id);
        }

        public List<Projeto> listar()
        {
            return itens.ToList();
        }
    }

}
