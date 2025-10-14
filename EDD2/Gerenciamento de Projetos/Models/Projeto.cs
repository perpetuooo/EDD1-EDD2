using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_de_Projetos.Models
{
    public class Projeto
    {
        private int nextTarefaId = 1;
        public int id { get; set; }
        public string nome { get; set; }
        public List<Tarefa> tarefas { get; set; }

        public Projeto(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
            this.tarefas = new List<Tarefa>();
        }

        public void adicionarTarefa(Tarefa t)
        {
            t.id = nextTarefaId++;
            tarefas.Add(t);
        }

        public bool removerTarefa(Tarefa t)
        {
            return tarefas.RemoveAll(x => x.id == t.id) > 0;
        }

        public Tarefa? buscarTarefa(int idT)
        {
            return tarefas.FirstOrDefault(x => x.id == idT);
        }

        public List<Tarefa> tarefasPorStatus(string s)
        {
            return tarefas.Where(x => x.status == s).ToList();
        }

        public List<Tarefa> tarefasPorPrioridade(int p)
        {
            return tarefas.Where(x => x.prioridade == p).ToList();
        }

        public int totalAbertas()
        {
            return tarefas.Count(x => x.status == "Aberta");
        }

        public int totalFechadas()
        {
            return tarefas.Count(x => x.status == "Fechada");
        }
    }
}
