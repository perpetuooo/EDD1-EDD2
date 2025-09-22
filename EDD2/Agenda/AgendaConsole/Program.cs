// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaConsole
{
    internal class Program
    {
        private static Contatos bancoContatos = new Contatos();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Agenda de Contatos (Console) ===");
            while (true)
            {
                MostrarMenu();
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine()?.Trim();

                try
                {
                    switch (opcao)
                    {
                        case "0":
                            Console.WriteLine("Saindo... Até logo!");
                            return;
                        case "1":
                            OpcaoAdicionarContato();
                            break;
                        case "2":
                            OpcaoPesquisarContato();
                            break;
                        case "3":
                            OpcaoAlterarContato();
                            break;
                        case "4":
                            OpcaoRemoverContato();
                            break;
                        case "5":
                            OpcaoListarContatos();
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.WriteLine("------------------------------");
        }

        private static void OpcaoAdicionarContato()
        {
            Console.WriteLine("--- Adicionar contato ---");

            var contato = LerContatoBasico();

            // telefones
            Console.Write("Quantos telefones deseja adicionar? (0): ");
            if (!int.TryParse(Console.ReadLine(), out var quantidadeTelefones)) quantidadeTelefones = 0;

            for (int i = 0; i < quantidadeTelefones; i++)
            {
                Console.WriteLine($"Telefone #{i + 1}");
                Console.Write("Tipo (ex: celular): ");
                var tipo = Console.ReadLine() ?? string.Empty;
                Console.Write("Número: ");
                var numero = Console.ReadLine() ?? string.Empty;
                Console.Write("É o telefone principal? (s/n): ");
                var respPrincipal = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
                var principal = respPrincipal == "s" || respPrincipal == "sim";

                contato.AdicionarTelefone(new Telefone(tipo, numero, principal));
            }

            var adicionou = bancoContatos.Adicionar(contato);
            Console.WriteLine(adicionou ? "Contato adicionado com sucesso." : "Já existe um contato com esse email. Adição cancelada.");
        }

        private static void OpcaoPesquisarContato()
        {
            Console.WriteLine("--- Pesquisar contato ---");
            Console.Write("Pesquisar por (1) Email ou (2) Nome? (1/2): ");
            var escolha = (Console.ReadLine() ?? "1").Trim();

            Contato? encontrado = null;
            if (escolha == "2")
            {
                Console.Write("Digite o nome (ou parte do nome): ");
                var nome = Console.ReadLine() ?? string.Empty;
                var chave = new Contato { Nome = nome };
                encontrado = bancoContatos.Pesquisar(chave);
            }
            else
            {
                Console.Write("Digite o email: ");
                var email = Console.ReadLine() ?? string.Empty;
                var chave = new Contato { Email = email };
                encontrado = bancoContatos.Pesquisar(chave);
            }

            if (encontrado == null)
            {
                Console.WriteLine("Contato não encontrado.");
            }
            else
            {
                Console.WriteLine("Contato encontrado:");
                Console.WriteLine(encontrado);
            }
        }

        private static void OpcaoAlterarContato()
        {
            Console.WriteLine("--- Alterar contato ---");
            Console.Write("Digite o email do contato a alterar: ");
            var emailBusca = Console.ReadLine() ?? string.Empty;
            var existente = bancoContatos.Pesquisar(new Contato { Email = emailBusca });

            if (existente == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("Contato atual:");
            Console.WriteLine(existente);
            Console.WriteLine();
            Console.WriteLine("Digite os novos valores (deixe em branco para manter o valor atual).");

            Console.Write($"Nome ({existente.Nome}): ");
            var novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome)) existente.Nome = novoNome.Trim();

            Console.Write($"Email ({existente.Email}): ");
            var novoEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoEmail)) existente.Email = novoEmail.Trim();

            Console.Write($"Dia de nascimento ({existente.DtNasc.Dia}): ");
            var entrada = Console.ReadLine();
            Console.Write($"Mês de nascimento ({existente.DtNasc.Mes}): ");
            var entrada2 = Console.ReadLine();
            Console.Write($"Ano de nascimento ({existente.DtNasc.Ano}): ");
            var entrada3 = Console.ReadLine();
            if (int.TryParse(entrada, out var d) && int.TryParse(entrada2, out var m) && int.TryParse(entrada3, out var a))
            {
                existente.DtNasc.SetData(d, m, a);
            }

            Console.Write("Deseja alterar os telefones? (s/n): ");
            var alterarTelefones = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
            if (alterarTelefones == "s" || alterarTelefones == "sim")
            {
                // reconstrói lista de telefones
                var novaLista = new List<Telefone>();
                Console.Write("Quantos telefones terá agora? (0): ");
                if (!int.TryParse(Console.ReadLine(), out var qtd)) qtd = 0;
                for (int i = 0; i < qtd; i++)
                {
                    Console.WriteLine($"Telefone #{i + 1}");
                    Console.Write("Tipo: ");
                    var tipo = Console.ReadLine() ?? string.Empty;
                    Console.Write("Número: ");
                    var numero = Console.ReadLine() ?? string.Empty;
                    Console.Write("É o principal? (s/n): ");
                    var resp = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
                    var principal = resp == "s" || resp == "sim";
                    novaLista.Add(new Telefone(tipo, numero, principal));
                }

                // aplica nova lista, garantindo apenas 1 principal (o primeiro marcado)
                bool principalEncontrado = false;
                foreach (var tel in novaLista)
                {
                    if (tel.Principal && !principalEncontrado)
                    {
                        principalEncontrado = true;
                    }
                    else
                    {
                        tel.Principal = false;
                    }
                }

                // Se não há principal e existe pelo menos 1 telefone, define o primeiro como principal
                if (!principalEncontrado && novaLista.Any())
                {
                    novaLista[0].Principal = true;
                }

                // cria contato atualizado (mantendo nome/email/dtNasc)
                var contatoAtualizado = new Contato(existente.Nome, existente.Email, existente.DtNasc);
                foreach (var tel in novaLista)
                    contatoAtualizado.AdicionarTelefone(tel);

                bancoContatos.Alterar(contatoAtualizado);
                Console.WriteLine("Telefones atualizados.");
                return;
            }

            // caso não altere telefones, apenas substitui o contato (email/nome/dtNasc já alterados)
            bancoContatos.Alterar(existente);
            Console.WriteLine("Contato alterado com sucesso.");
        }

        private static void OpcaoRemoverContato()
        {
            Console.WriteLine("--- Remover contato ---");
            Console.Write("Digite o email do contato a remover: ");
            var email = Console.ReadLine() ?? string.Empty;
            var contato = bancoContatos.Pesquisar(new Contato { Email = email });

            if (contato == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("Contato encontrado:");
            Console.WriteLine(contato);
            Console.Write("Confirma remoção? (s/n): ");
            var confirma = (Console.ReadLine() ?? "n").Trim().ToLowerInvariant();
            if (confirma == "s" || confirma == "sim")
            {
                var removido = bancoContatos.Remover(contato);
                Console.WriteLine(removido ? "Contato removido." : "Falha ao remover contato.");
            }
            else
            {
                Console.WriteLine("Remoção cancelada.");
            }
        }

        private static void OpcaoListarContatos()
        {
            Console.WriteLine("--- Lista de contatos ---");
            var lista = bancoContatos.Agenda;
            if (!lista.Any())
            {
                Console.WriteLine("Nenhum contato cadastrado.");
                return;
            }

            int i = 1;
            foreach (var contato in lista.OrderBy(c => c.Nome))
            {
                Console.WriteLine($"--- Contato #{i} ---");
                Console.WriteLine(contato);
                Console.WriteLine();
                i++;
            }
        }

        private static Contato LerContatoBasico()
        {
            Console.Write("Nome: ");
            var nome = Console.ReadLine() ?? string.Empty;
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Data de nascimento:");
            int dia = LerInteiro("Dia (1-31): ", 1, 31);
            int mes = LerInteiro("Mês (1-12): ", 1, 12);
            int ano = LerInteiro("Ano (ex: 1990): ", 1, 9999);

            var data = new Data(dia, mes, ano);
            var contato = new Contato(nome, email, data);
            return contato;
        }

        private static int LerInteiro(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var texto = Console.ReadLine();
                if (int.TryParse(texto, out var valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine("Valor inválido. Tente novamente.");
            }
        }
    }
}
