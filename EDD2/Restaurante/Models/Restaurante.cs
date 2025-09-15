// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteMVC.Models
{
    class Restaurante
    {
        public int proxPedido = 1;
        public Pedido[] Pedidos = new Pedido[50];


        public bool Novo(Pedido p)
        {
            for (int i = 0; i < Pedidos.Length; i++)
            {
                if (Pedidos[i] == null)
                {
                    p.Id = proxPedido++;
                    Pedidos[i] = p;
                    return true;
                }
            }
            return false;
        }


        public Pedido Buscar(int id)
        {
            foreach (var p in Pedidos)
            {
                if (p != null && p.Id == id) return p;
            }
            return null;
        }


        public bool Cancelar(int id)
        {
            for (int i = 0; i < Pedidos.Length; i++)
            {
                if (Pedidos[i] != null && Pedidos[i].Id == id)
                {
                    Pedidos[i] = null;
                    return true;
                }
            }
            return false;
        }


        public Pedido[] Todos()
        {
            return Pedidos;
        }
    }
}