using System;
using System.Collections.Generic;

namespace App
{
    public class Lote
    {
        public int id;
        public int qtde;
        public DateTime venc;

        public Lote() { }

        public Lote(int id, int qtde, DateTime venc)
        {
            this.id = id;
            this.qtde = qtde;
            this.venc = venc;
        }

        public override string ToString() => $"{id}-{qtde}-{venc:yyyy-MM-dd}";
    }

    public class Medicamento
    {
        public int id;
        public string nome;
        public string laboratorio;
        public Queue<Lote> lotes;

        public Medicamento()
        {
            lotes = new Queue<Lote>();
        }

        public Medicamento(int id, string nome, string laboratorio)
        {
            this.id = id;
            this.nome = nome;
            this.laboratorio = laboratorio;
            lotes = new Queue<Lote>();
        }

        public int qtdeDisponivel()
        {
            int s = 0;
            foreach (var l in lotes) s += l.qtde;
            return s;
        }

        public void comprar(Lote lote)
        {
            lotes.Enqueue(lote);
        }

        public bool vender(int qtde)
        {
            if (qtdeDisponivel() < qtde) return false;
            int need = qtde;
            while (need > 0 && lotes.Count > 0)
            {
                var l = lotes.Peek();
                if (l.qtde <= need)
                {
                    need -= l.qtde;
                    lotes.Dequeue();
                }
                else
                {
                    l.qtde -= need;
                    need = 0;
                }
            }
            return true;
        }

        public override string ToString() => $"{id}-{nome}-{laboratorio}-{qtdeDisponivel()}";

        public override bool Equals(object obj)
        {
            var m = obj as Medicamento;
            if (m == null) return false;
            return this.id == m.id;
        }

        public override int GetHashCode() => id.GetHashCode();
    }

    public class Medicamentos
    {
        public List<Medicamento> listaMedicamentos;

        public Medicamentos()
        {
            listaMedicamentos = new List<Medicamento>();
        }

        public void adicionar(Medicamento medicamento)
        {
            listaMedicamentos.Add(medicamento);
        }

        public bool deletar(Medicamento medicamento)
        {
            var m = pesquisar(medicamento);
            if (m == null || m.id == 0) return false;
            if (m.qtdeDisponivel() == 0) return listaMedicamentos.Remove(m);
            return false;
        }

        public Medicamento pesquisar(Medicamento medicamento)
        {
            var m = listaMedicamentos.Find(x => x.id == medicamento.id);
            if (m == null) return new Medicamento();
            return m;
        }
    }
}
