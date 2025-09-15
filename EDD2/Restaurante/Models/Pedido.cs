// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteMVC.Models
{
    class Pedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public Item[] Itens { get; private set; } = new Item[10];
        private int proxItem = 1;


        public bool Add(string desc, double preco)
        {
            for (int i = 0; i < Itens.Length; i++)
            {
                if (Itens[i] == null)
                {
                    Itens[i] = new Item { Id = proxItem++, Descricao = desc, Preco = preco };
                    return true;
                }
            }
            return false;
        }


        public bool Rem(int itemId)
        {
            for (int i = 0; i < Itens.Length; i++)
            {
                if (Itens[i] != null && Itens[i].Id == itemId)
                {
                    Itens[i] = null;
                    return true;
                }
            }
            return false;
        }


        public double Total()
        {
            double total = 0;
            foreach (var it in Itens)
            {
                if (it != null) total += it.Preco;
            }
            return total;
        }


        public string Mostrar()
        {
            string s = $"Pedido {Id} - Cliente: {Cliente}\n";
            s += "Itens:\n";
            foreach (var it in Itens)
            {
                if (it != null)
                {
                    s += $" [{it.Id}] {it.Descricao} - R$ {it.Preco:F2}\n";
                }
            }
            s += $"Total: R$ {Total():F2}\n";
            return s;
        }
    }
}