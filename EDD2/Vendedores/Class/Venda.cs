// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Class
{
    public class Venda
    {
        public int Qtde { get; set; }
        public double Valor { get; set; }


        public Venda() { }
        public Venda(int qtde, double valor)
        {
            Qtde = qtde;
            Valor = valor;
        }


        public double ValorMedio()
        {
            if (Qtde == 0) return 0.0;
            return Valor / Qtde;
        }
    }
}
