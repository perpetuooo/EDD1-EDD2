using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento_de_Projetos.Models
{
    public class Tarefa
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int prioridade { get; set; }
        public string status { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataConclusao { get; set; }

        public Tarefa(int id, string titulo, string descricao, int prioridade)
        {
            this.id = id;
            this.titulo = titulo;
            this.descricao = descricao;
            this.prioridade = prioridade;
            this.status = "Aberta";
            this.dataCriacao = DateTime.Now;
            this.dataConclusao = DateTime.MinValue;
        }

        public void concluir()
        {
            if (status != "Fechada")
            {
                status = "Fechada";
                dataConclusao = DateTime.Now;
            }
        }

        public void cancelar()
        {
            if (status != "Cancelada")
            {
                status = "Cancelada";
                dataConclusao = DateTime.Now;
            }
        }

        public void reabrir()
        {
            status = "Aberta";
            dataConclusao = DateTime.MinValue;
        }
    }

}
