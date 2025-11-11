using System;
using App;

class Program
{
    static void Main()
    {
        var db = new Medicamentos();

        while (true)
        {
            Console.WriteLine("0. Finalizar processo");
            Console.WriteLine("1. Cadastrar medicamento");
            Console.WriteLine("2. Consultar medicamento (sintético)");
            Console.WriteLine("3. Consultar medicamento (analítico)");
            Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
            Console.WriteLine("5. Vender medicamento");
            Console.WriteLine("6. Listar medicamentos");
            Console.Write("\nEscolha: ");

            string op = Console.ReadLine();
            Console.WriteLine();

            if (op == "0")
                break;

            if (op == "1")
            {
                Console.Write("id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("nome: ");
                string nome = Console.ReadLine();

                Console.Write("laboratorio: ");
                string lab = Console.ReadLine();

                db.adicionar(new Medicamento(id, nome, lab));

                Console.WriteLine("\nMedicamento cadastrado.\n");
            }
            else if (op == "2")
            {
                Console.Write("id: ");
                int id = int.Parse(Console.ReadLine());

                var found = db.pesquisar(new Medicamento { id = id });

                if (found.id == 0)
                    Console.WriteLine("\nNão encontrado.\n");
                else
                    Console.WriteLine($"\n{found}\n");
            }
            else if (op == "3")
            {
                Console.Write("id: ");
                int id = int.Parse(Console.ReadLine());

                var found = db.pesquisar(new Medicamento { id = id });

                if (found.id == 0)
                    Console.WriteLine("\nNão encontrado.\n");
                else
                {
                    Console.WriteLine($"\n{found}\n");

                    if (found.lotes.Count == 0)
                        Console.WriteLine("Sem lotes.\n");
                    else
                    {
                        foreach (var l in found.lotes)
                            Console.WriteLine(l);
                        Console.WriteLine();
                    }
                }
            }
            else if (op == "4")
            {
                Console.Write("id do medicamento: ");
                int idm = int.Parse(Console.ReadLine());

                var med = db.pesquisar(new Medicamento { id = idm });

                if (med.id == 0)
                {
                    Console.WriteLine("\nMedicamento não encontrado.\n");
                    continue;
                }

                Console.Write("id do lote: ");
                int idl = int.Parse(Console.ReadLine());

                Console.Write("qtde do lote: ");
                int ql = int.Parse(Console.ReadLine());

                Console.Write("data de vencimento (yyyy-mm-dd): ");
                DateTime dv = DateTime.Parse(Console.ReadLine());

                med.comprar(new Lote(idl, ql, dv));

                Console.WriteLine("\nLote adicionado.\n");
            }
            else if (op == "5")
            {
                Console.Write("id do medicamento: ");
                int idm = int.Parse(Console.ReadLine());

                Console.Write("qtde a vender: ");
                int qv = int.Parse(Console.ReadLine());

                var med = db.pesquisar(new Medicamento { id = idm });

                if (med.id == 0)
                {
                    Console.WriteLine("\nMedicamento não encontrado.\n");
                    continue;
                }

                bool ok = med.vender(qv);

                Console.WriteLine(ok ? "\nVenda realizada.\n" : "\nSaldo insuficiente.\n");
            }
            else if (op == "6")
            {
                if (db.listaMedicamentos.Count == 0)
                {
                    Console.WriteLine("\nNenhum medicamento.\n");
                    continue;
                }

                foreach (var m in db.listaMedicamentos)
                    Console.WriteLine(m);

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nOpção inválida.\n");
            }
        }
    }
}
