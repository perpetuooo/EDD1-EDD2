// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using RestauranteMVC.Models;

namespace RestauranteMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            var rest = new Restaurante();
            while (true)
            {
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Criar novo pedido");
                Console.WriteLine("2. Adicionar item ao pedido");
                Console.WriteLine("3. Remover item do pedido");
                Console.WriteLine("4. Consultar pedido");
                Console.WriteLine("5. Cancelar pedido");
                Console.WriteLine("6. Listar todos os pedidos");
                Console.Write("Escolha: ");
                int op = int.Parse(Console.ReadLine());
                if (op == 0) break;
                if (op == 1)
                {
                    Console.Write("Nome do cliente: ");
                    var c = Console.ReadLine();
                    var p = new Pedido { Cliente = c };
                    rest.Novo(p);
                }
                if (op == 2)
                {
                    Console.Write("Id do pedido: ");
                    int id = int.Parse(Console.ReadLine());
                    var ped = rest.Buscar(id);
                    Console.Write("Descrição do item: ");
                    var d = Console.ReadLine();
                    Console.Write("Preço do item: ");
                    double pr = double.Parse(Console.ReadLine());
                    ped.Add(d, pr);
                }
                if (op == 3)
                {
                    Console.Write("Id do pedido: ");
                    int id = int.Parse(Console.ReadLine());
                    var ped = rest.Buscar(id);
                    Console.Write("Id do item: ");
                    int iid = int.Parse(Console.ReadLine());
                    ped.Rem(iid);
                }
                if (op == 4)
                {
                    Console.Write("Id do pedido: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine(rest.Buscar(id).Mostrar());
                }
                if (op == 5)
                {
                    Console.Write("Id do pedido: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Cancelar(id);
                }
                if (op == 6)
                {
                    double soma = 0;
                    foreach (var ped in rest.Todos())
                    {
                        if (ped != null)
                        {
                            double t = ped.Total();
                            soma += t;
                            Console.WriteLine($"Pedido {ped.Id} - Total: R$ {t:F2}");
                        }
                    }
                    Console.WriteLine($"Total do dia: R$ {soma:F2}");
                }
            }
        }
    }
}