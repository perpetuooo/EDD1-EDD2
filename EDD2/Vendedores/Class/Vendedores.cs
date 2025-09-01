// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Class
{
    public class Vendedores
    {
        public Vendedor[] OsVendedores { get; set; }
        public int Max { get; set; }
        public int Qtde { get; set; }


        public Vendedores(int max)
        {
            Max = max;
            OsVendedores = new Vendedor[max];
            Qtde = 0;
        }


        public bool AddVendedor(Vendedor v)
        {
            if (Qtde >= Max) return false;
            for (int i = 0; i < Max; i++)
            {
                if (OsVendedores[i] == null)
                {
                    OsVendedores[i] = v;
                    Qtde++;
                    return true;
                }
            }
            return false;
        }

        public bool DelVendedor(Vendedor v)
        {
            for (int i = 0; i < Max; i++)
            {
                if (OsVendedores[i] != null && OsVendedores[i].Id == v.Id)
                {
                    if (OsVendedores[i].PossuiVendas()) return false;
                    OsVendedores[i] = null;
                    Qtde--;
                    return true;
                }
            }
            return false;
        }

        public Vendedor SearchVendedor(int id)
        {
            for (int i = 0; i < Max; i++)
            {
                if (OsVendedores[i] != null && OsVendedores[i].Id == id) return OsVendedores[i];
            }
            return null;
        }

        public double ValorVendas()
        {
            double total = 0.0;
            foreach (var v in OsVendedores)
                if (v != null) total += v.ValorVendas();
            return total;
        }


        public double ValorComissao()
        {
            double total = 0.0;
            foreach (var v in OsVendedores)
                if (v != null) total += v.ValorComissao();
            return total;
        }
    }
}
