using System;
using System.Linq;

namespace BibliotecaApp
{
    class Program
    {
        static Livros biblioteca = new Livros();

        static void Main()
        {
            SeedDadosIniciais();

            while (true)
            {
                ExibirMenu();
                int opcao = LerInteiro("Escolha uma opção: ");

                Console.WriteLine();
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo... até logo!");
                        return;
                    case 1:
                        OpcaoAdicionarLivro();
                        break;
                    case 2:
                        OpcaoPesquisarLivroSintetico();
                        break;
                    case 3:
                        OpcaoPesquisarLivroAnalitico();
                        break;
                    case 4:
                        OpcaoAdicionarExemplar();
                        break;
                    case 5:
                        OpcaoRegistrarEmprestimo();
                        break;
                    case 6:
                        OpcaoRegistrarDevolucao();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ExibirMenu()
        {
            Console.WriteLine("==== Biblioteca - Menu ====");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar livro");
            Console.WriteLine("2. Pesquisar livro (sintético)");
            Console.WriteLine("3. Pesquisar livro (analítico)");
            Console.WriteLine("4. Adicionar exemplar");
            Console.WriteLine("5. Registrar empréstimo");
            Console.WriteLine("6. Registrar devolução");
            Console.WriteLine("===========================");
        }

        #region Menu Options

        static void OpcaoAdicionarLivro()
        {
            Console.WriteLine("== Adicionar Livro ==");
            int isbn = LerInteiro("ISBN (número): ");
            string titulo = LerTexto("Título: ");
            string autor = LerTexto("Autor: ");
            string editora = LerTexto("Editora: ");

            if (biblioteca.PesquisarPorIsbn(isbn) != null)
            {
                Console.WriteLine("Já existe um livro com esse ISBN no acervo.");
                return;
            }

            var novoLivro = new Livro(isbn, titulo, autor, editora);
            biblioteca.Adicionar(novoLivro);

            Console.WriteLine("Livro adicionado com sucesso.");
        }

        static void OpcaoPesquisarLivroSintetico()
        {
            Console.WriteLine("== Pesquisa (sintético) ==");
            var livro = LocalizarLivroInterativo();
            if (livro == null) return;

            Console.WriteLine();
            Console.WriteLine("DADOS SINTÉTICOS:");
            Console.WriteLine(livro.ToString());
            Console.WriteLine($"Total de exemplares: {livro.QtdeExemplares()}");
            Console.WriteLine($"Exemplares disponíveis: {livro.QtdeDisponiveis()}");
            Console.WriteLine($"Total de empréstimos (histórico): {livro.QtdeEmprestimos()}");
            Console.WriteLine($"Percentual de disponibilidade: {livro.PercDisponibilidade():F2}%");
        }

        static void OpcaoPesquisarLivroAnalitico()
        {
            Console.WriteLine("== Pesquisa (analítico) ==");
            var livro = LocalizarLivroInterativo();
            if (livro == null) return;

            Console.WriteLine();
            Console.WriteLine("DADOS ANALÍTICOS:");
            Console.WriteLine(livro.ToString());
            Console.WriteLine($"Total de exemplares: {livro.QtdeExemplares()}");
            Console.WriteLine($"Exemplares disponíveis: {livro.QtdeDisponiveis()}");
            Console.WriteLine($"Total de empréstimos (histórico): {livro.QtdeEmprestimos()}");
            Console.WriteLine($"Percentual de disponibilidade: {livro.PercDisponibilidade():F2}%");
            Console.WriteLine();

            var exemplares = livro.GetExemplares().OrderBy(e => e.Tombo).ToList();
            if (!exemplares.Any())
            {
                Console.WriteLine("Este livro não possui exemplares cadastrados.");
                return;
            }

            foreach (var ex in exemplares)
            {
                Console.WriteLine($"--- Exemplar Tombo: {ex.Tombo} ---");
                Console.WriteLine($"Disponível? {(ex.Disponivel() ? "Sim" : "Não")}");
                Console.WriteLine($"Quantidade de empréstimos: {ex.QtdeEmprestimos()}");
                var emprestimos = ex.GetEmprestimos();
                if (!emprestimos.Any())
                {
                    Console.WriteLine("  Sem empréstimos registrados para este exemplar.");
                }
                else
                {
                    Console.WriteLine("  Histórico de empréstimos:");
                    foreach (var emp in emprestimos)
                    {
                        string devolucao = emp.DtDevolucao.HasValue ? emp.DtDevolucao.Value.ToString("g") : "EM ABERTO";
                        Console.WriteLine($"    - Emprestado em {emp.DtEmprestimo:g} | Devolução: {devolucao}");
                    }
                }
                Console.WriteLine();
            }
        }

        static void OpcaoAdicionarExemplar()
        {
            Console.WriteLine("== Adicionar Exemplar ==");
            var livro = LocalizarLivroInterativo();
            if (livro == null) return;

            int proximoTombo = (livro.GetExemplares().Any() ? livro.GetExemplares().Max(e => e.Tombo) + 1 : 1);
            var novoExemplar = new Exemplar(proximoTombo);
            livro.AdicionarExemplar(novoExemplar);

            Console.WriteLine($"Exemplar adicionado com sucesso. Tombo = {proximoTombo}");
        }

        static void OpcaoRegistrarEmprestimo()
        {
            Console.WriteLine("== Registrar Empréstimo ==");
            var livro = LocalizarLivroInterativo();
            if (livro == null) return;

            var exemplaresDisponiveis = livro.GetExemplares().Where(e => e.Disponivel()).ToList();
            if (!exemplaresDisponiveis.Any())
            {
                Console.WriteLine("Não há exemplares disponíveis para empréstimo deste livro.");
                return;
            }

            Console.WriteLine("Exemplares disponíveis:");
            foreach (var ex in exemplaresDisponiveis)
                Console.WriteLine($"- Tombo: {ex.Tombo}");

            int tomboEscolhido = LerInteiro("Digite o tombo do exemplar que deseja emprestar: ");
            var exemplarSelecionado = exemplaresDisponiveis.FirstOrDefault(e => e.Tombo == tomboEscolhido);
            if (exemplarSelecionado == null)
            {
                Console.WriteLine("Tombo inválido ou exemplar não disponível.");
                return;
            }

            bool ok = exemplarSelecionado.Emprestar();
            Console.WriteLine(ok ? "Empréstimo registrado com sucesso." : "Falha ao registrar empréstimo.");
        }

        static void OpcaoRegistrarDevolucao()
        {
            Console.WriteLine("== Registrar Devolução ==");
            int isbn = LerInteiro("ISBN do livro: ");
            var livro = biblioteca.PesquisarPorIsbn(isbn);
            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            var exemplaresEmprestados = livro.GetExemplares().Where(e => !e.Disponivel()).ToList();
            if (!exemplaresEmprestados.Any())
            {
                Console.WriteLine("Não há exemplares emprestados deste livro.");
                return;
            }

            Console.WriteLine("Exemplares emprestados (em aberto):");
            foreach (var ex in exemplaresEmprestados)
                Console.WriteLine($"- Tombo: {ex.Tombo}");

            int tomboEscolhido = LerInteiro("Digite o tombo do exemplar que será devolvido: ");
            var exemplarSelecionado = exemplaresEmprestados.FirstOrDefault(e => e.Tombo == tomboEscolhido);
            if (exemplarSelecionado == null)
            {
                Console.WriteLine("Tombo inválido ou exemplar não está emprestado.");
                return;
            }

            bool ok = exemplarSelecionado.Devolver();
            Console.WriteLine(ok ? "Devolução registrada com sucesso." : "Falha ao registrar devolução.");
        }

        #endregion

        #region Helpers & Input

        static Livro? LocalizarLivroInterativo()
        {
            Console.WriteLine("Busque por: 1) ISBN  2) Título (parcial)");
            int escolha = LerInteiro("Escolha busca (1 ou 2): ");
            if (escolha == 1)
            {
                int isbn = LerInteiro("Informe o ISBN: ");
                var livro = biblioteca.PesquisarPorIsbn(isbn);
                if (livro == null)
                    Console.WriteLine("Livro não encontrado.");
                return livro;
            }
            else if (escolha == 2)
            {
                string termo = LerTexto("Informe parte do título: ");
                var resultados = biblioteca.PesquisarPorTitulo(termo).ToList();
                if (!resultados.Any())
                {
                    Console.WriteLine("Nenhum livro encontrado com esse título.");
                    return null;
                }

                Console.WriteLine("Resultados:");
                for (int i = 0; i < resultados.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {resultados[i].ToString()}");
                }

                int indice = LerInteiro($"Escolha o número do livro (1..{resultados.Count}): ");
                if (indice < 1 || indice > resultados.Count)
                {
                    Console.WriteLine("Escolha inválida.");
                    return null;
                }

                return resultados[indice - 1];
            }
            else
            {
                Console.WriteLine("Opção de busca inválida.");
                return null;
            }
        }

        static int LerInteiro(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? entrada = Console.ReadLine();
                if (int.TryParse(entrada, out int valor))
                    return valor;
                Console.WriteLine("Entrada inválida. Digite um número inteiro.");
            }
        }

        static string LerTexto(string prompt)
        {
            Console.Write(prompt);
            string? texto = Console.ReadLine();
            return texto?.Trim() ?? string.Empty;
        }

        #endregion

        #region Seed Data (optional)

        static void SeedDadosIniciais()
        {
            var l1 = new Livro(9780131103627, "The C Programming Language", "Kernighan & Ritchie", "Prentice Hall");
            l1.AdicionarExemplar(new Exemplar(1));
            l1.AdicionarExemplar(new Exemplar(2));
            biblioteca.Adicionar(l1);

            var l2 = new Livro(9780201633610, "Design Patterns", "Gamma, Helm, Johnson, Vlissides", "Addison-Wesley");
            l2.AdicionarExemplar(new Exemplar(1));
            biblioteca.Adicionar(l2);

            var exemplarParaEmprestar = l1.GetExemplares().First();
            exemplarParaEmprestar.Emprestar();
        }

        #endregion
    }
}
