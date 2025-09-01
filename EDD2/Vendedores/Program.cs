// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using Vendas.Class;


namespace Vendas
{
    class Program
    {
        static Vendedores repo = new Vendedores(10); // limite 10
        static int nextId = 1;


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("--- Vendedores ---");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.WriteLine();

                Console.Write("Opção: ");
                var op = Console.ReadLine();
                Console.WriteLine();
                if (op == "0") { Console.WriteLine("Saindo..."); break; }
                switch (op)
                {
                    case "1": CadastrarVendedor(); break;
                    case "2": ConsultarVendedor(); break;
                    case "3": ExcluirVendedor(); break;
                    case "4": RegistrarVenda(); break;
                    case "5": ListarVendedores(); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
                Console.Clear();
            }
        }

        static void CadastrarVendedor()
        {
            if (repo.Qtde >= repo.Max)
            {
                Console.WriteLine("Limite de vendedores atingido (máx {0}).", repo.Max);
                return;
            }
            var nome = Input.ReadString("Nome: ");
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }
            double perc = Input.ReadDouble("Percentual de comissão (ex: 10): ");
            var v = new Vendedor(nextId++, nome.Trim(), perc);
            if (repo.AddVendedor(v))
                Console.WriteLine("Vendedor cadastrado com Id {0}.", v.Id);
            else
                Console.WriteLine("Erro ao cadastrar vendedor.");
        }

        static void ConsultarVendedor()
        {
            int id = Input.ReadInt("Digite o Id do vendedor: ");
            var v = repo.SearchVendedor(id);
            if (v == null) { Console.WriteLine("Vendedor não encontrado."); return; }
            Console.WriteLine("Id: {0}", v.Id);
            Console.WriteLine("Nome: {0}", v.Nome);
            Console.WriteLine("Total Vendas: {0:F2}", v.ValorVendas());
            Console.WriteLine("Comissão Devida: {0:F2}", v.ValorComissao());
            Console.WriteLine("Valor médio diário (dias com venda): {0:F2}", v.ValorMedioDiario());
            Console.WriteLine();
            Console.WriteLine("Vendas por dia:");
            Console.WriteLine("Dia\tQtde\tValor\tValor Médio");
            for (int i = 0; i < 31; i++)
            {
                var venda = v.AsVendas[i];
                if (venda != null)
                {
                    Console.WriteLine("{0}\t{1}\t{2:F2}\t{3:F2}", i + 1, venda.Qtde, venda.Valor, venda.ValorMedio());
                }
            }
        }

        static void ExcluirVendedor()
        {
            int id = Input.ReadInt("Digite o Id do vendedor a excluir: ");
            var v = repo.SearchVendedor(id);
            if (v == null) { Console.WriteLine("Vendedor não encontrado."); return; }
            if (v.PossuiVendas())
            {
                Console.WriteLine("Não é possível excluir: vendedor possui vendas.");
                return;
            }
            var conf = Input.ReadString($"Confirma exclusão do vendedor {v.Nome} (s/n)? ");
            if (!string.IsNullOrEmpty(conf) && conf.ToLower().StartsWith("s"))
            {
                if (repo.DelVendedor(v)) Console.WriteLine("Vendedor excluído.");
                else Console.WriteLine("Erro ao excluir vendedor.");
            }
            else Console.WriteLine("Exclusão cancelada.");
        }

        static void RegistrarVenda()
        {
            int id = Input.ReadInt("Digite o Id do vendedor: ");
            var v = repo.SearchVendedor(id);
            if (v == null) { Console.WriteLine("Vendedor não encontrado."); return; }
            int dia = Input.ReadInt("Dia (1..31): ");
            if (dia < 1 || dia > 31) { Console.WriteLine("Dia inválido."); return; }
            int qtde = Input.ReadInt("Qtde: ");
            double valor = Input.ReadDouble("Valor (total do dia): ");
            var venda = new Venda(qtde, valor);
            v.RegistrarVenda(dia, venda);
            Console.WriteLine("Venda registrada no dia {0} para vendedor {1}.", dia, v.Nome);
        }

        static void ListarVendedores()
        {
            Console.WriteLine("Id\tNome\tTotal Vendas\tComissão");
            foreach (var v in repo.OsVendedores)
            {
                if (v == null) continue;
                Console.WriteLine("{0}\t{1}\t{2:F2}\t{3:F2}", v.Id, v.Nome, v.ValorVendas(), v.ValorComissao());
            }
            Console.WriteLine();
            Console.WriteLine("Total geral de vendas: {0:F2}", repo.ValorVendas());
            Console.WriteLine("Total geral de comissão: {0:F2}", repo.ValorComissao());
        }
    }
}