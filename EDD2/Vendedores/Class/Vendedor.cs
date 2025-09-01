// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Class
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double PercComissao { get; set; }
        public Venda[] AsVendas { get; set; } = new Venda[31];


        public Vendedor() { }
        public Vendedor(int id, string nome, double perc)
        {
            Id = id; Nome = nome; PercComissao = perc;
            for (int i = 0; i < 31; i++) AsVendas[i] = null;
        }


        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia < 1 || dia > 31) return;
            AsVendas[dia - 1] = venda;
        }


        public double ValorVendas()
        {
            double total = 0.0;
            foreach (var v in AsVendas)
                if (v != null) total += v.Valor;
            return total;
        }


        public double ValorComissao()
        {
            return ValorVendas() * (PercComissao / 100.0);
        }


        public double ValorMedioDiario()
        {
            double total = 0.0; int dias = 0;
            foreach (var v in AsVendas)
            {
                if (v != null)
                {
                    total += v.Valor;
                    dias++;
                }
            }
            if (dias == 0) return 0.0;
            return total / dias;
        }


        public bool PossuiVendas()
        {
            foreach (var v in AsVendas) if (v != null) return true;
            return false;
        }
    }
}
