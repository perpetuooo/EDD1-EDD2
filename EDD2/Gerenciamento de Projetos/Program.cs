using Gerenciamento_de_Projetos.Models;
using System;
using System.Linq;

class Program
{
    static Projetos projetos = new Projetos();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("0 Sair");
            Console.WriteLine("1 Adicionar projeto");
            Console.WriteLine("2 Pesquisar projeto (mostrar tarefas por status e totais)");
            Console.WriteLine("3 Remover projeto (apenas se sem tarefas)");
            Console.WriteLine("4 Adicionar tarefa em projeto");
            Console.WriteLine("5 Concluir tarefa");
            Console.WriteLine("6 Cancelar tarefa");
            Console.WriteLine("7 Reabrir tarefa");
            Console.WriteLine("8 Listar tarefas de um projeto");
            Console.WriteLine("9 Filtrar tarefas por status ou prioridade em um projeto");
            Console.WriteLine("10 Filtrar tarefas por status ou prioridade em todos os projetos");
            Console.WriteLine("11 Resumo geral");
            Console.Write("Opção: ");
            var op = Console.ReadLine();
            if (!int.TryParse(op, out int o)) continue;
            if (o == 0) break;
            switch (o)
            {
                case 1: Op1(); break;
                case 2: Op2(); break;
                case 3: Op3(); break;
                case 4: Op4(); break;
                case 5: Op5(); break;
                case 6: Op6(); break;
                case 7: Op7(); break;
                case 8: Op8(); break;
                case 9: Op9(); break;
                case 10: Op10(); break;
                case 11: Op11(); break;
            }
        }
    }

    static Projeto? SelecionarProjeto()
    {
        var lista = projetos.listar();
        if (!lista.Any())
        {
            Console.WriteLine("Nenhum projeto.");
            return null;
        }
        foreach (var p in lista) Console.WriteLine($"[{p.id}] {p.nome} (tarefas {p.tarefas.Count})");
        Console.Write("Id do projeto: ");
        var t = Console.ReadLine();
        if (!int.TryParse(t, out int id)) return null;
        return projetos.buscar(id);
    }

    static void Op1()
    {
        Console.Write("Nome do projeto: ");
        var nome = Console.ReadLine() ?? "";
        var p = new Projeto(0, nome);
        projetos.adicionar(p);
        Console.WriteLine($"Projeto criado id {p.id}");
    }

    static void Op2()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        Console.WriteLine($"Projeto {p.id} {p.nome}");
        var estados = new[] { "Aberta", "Fechada", "Cancelada" };
        foreach (var s in estados)
        {
            var lis = p.tarefasPorStatus(s);
            Console.WriteLine($"{s}: {lis.Count}");
            foreach (var t in lis) Console.WriteLine($"  [{t.id}] {t.titulo} ({t.prioridade}) - {t.status}");
        }
        Console.WriteLine($"Totais abertas: {p.totalAbertas()} fechadas: {p.totalFechadas()}");
    }

    static void Op3()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        if (p.tarefas.Count > 0)
        {
            Console.WriteLine("Remoção negada: projeto tem tarefas.");
            return;
        }
        projetos.remover(p);
        Console.WriteLine("Projeto removido.");
    }

    static void Op4()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        Console.Write("Título: ");
        var t = Console.ReadLine() ?? "";
        Console.Write("Descrição: ");
        var d = Console.ReadLine() ?? "";
        Console.Write("Prioridade (1=alta 2=média 3=baixa): ");
        var pr = Console.ReadLine();
        if (!int.TryParse(pr, out int pi)) pi = 3;
        var tarefa = new Tarefa(0, t, d, pi);
        p.adicionarTarefa(tarefa);
        Console.WriteLine($"Tarefa adicionada id {tarefa.id}");
    }

    static void Op5()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        var tar = SelecionarTarefa(p);
        if (tar == null) return;
        tar.concluir();
        Console.WriteLine("Tarefa concluída.");
    }

    static void Op6()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        var tar = SelecionarTarefa(p);
        if (tar == null) return;
        tar.cancelar();
        Console.WriteLine("Tarefa cancelada.");
    }

    static void Op7()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        var tar = SelecionarTarefa(p);
        if (tar == null) return;
        tar.reabrir();
        Console.WriteLine("Tarefa reaberta.");
    }

    static void Op8()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        if (!p.tarefas.Any()) { Console.WriteLine("Sem tarefas."); return; }
        foreach (var t in p.tarefas) Console.WriteLine($"[{t.id}] {t.titulo} - {t.status} - prio {t.prioridade}");
    }

    static void Op9()
    {
        var p = SelecionarProjeto();
        if (p == null) return;
        Console.WriteLine("1 filtrar por status 2 por prioridade");
        var escolha = Console.ReadLine();
        if (escolha == "1")
        {
            Console.Write("Status (Aberta/Fechada/Cancelada): ");
            var s = Console.ReadLine() ?? "";
            var lis = p.tarefasPorStatus(s);
            foreach (var t in lis) Console.WriteLine($"[{t.id}] {t.titulo} - {t.status} - prio {t.prioridade}");
        }
        else
        {
            Console.Write("Prioridade (1/2/3): ");
            var pr = Console.ReadLine();
            if (!int.TryParse(pr, out int pi)) return;
            var lis = p.tarefasPorPrioridade(pi);
            foreach (var t in lis) Console.WriteLine($"[{t.id}] {t.titulo} - {t.status} - prio {t.prioridade}");
        }
    }

    static void Op10()
    {
        Console.WriteLine("1 filtrar por status 2 por prioridade");
        var escolha = Console.ReadLine();
        if (escolha == "1")
        {
            Console.Write("Status (Aberta/Fechada/Cancelada): ");
            var s = Console.ReadLine() ?? "";
            foreach (var p in projetos.listar())
            {
                var lis = p.tarefasPorStatus(s);
                if (!lis.Any()) continue;
                Console.WriteLine($"Projeto [{p.id}] {p.nome}");
                foreach (var t in lis) Console.WriteLine($"  [{t.id}] {t.titulo} - {t.status} - prio {t.prioridade}");
            }
        }
        else
        {
            Console.Write("Prioridade (1/2/3): ");
            var pr = Console.ReadLine();
            if (!int.TryParse(pr, out int pi)) return;
            foreach (var p in projetos.listar())
            {
                var lis = p.tarefasPorPrioridade(pi);
                if (!lis.Any()) continue;
                Console.WriteLine($"Projeto [{p.id}] {p.nome}");
                foreach (var t in lis) Console.WriteLine($"  [{t.id}] {t.titulo} - {t.status} - prio {t.prioridade}");
            }
        }
    }

    static void Op11()
    {
        var pts = projetos.listar();
        var totalProjetos = pts.Count;
        var abertas = pts.Sum(p => p.tarefas.Count(t => t.status == "Aberta"));
        var fechadas = pts.Sum(p => p.tarefas.Count(t => t.status == "Fechada"));
        var canceladas = pts.Sum(p => p.tarefas.Count(t => t.status == "Cancelada"));
        double perc = 0;
        if (abertas == 0)
        {
            perc = fechadas > 0 ? 100 : 0;
        }
        else
        {
            perc = (fechadas / (double)abertas) * 100.0;
        }
        Console.WriteLine($"Projetos: {totalProjetos}");
        Console.WriteLine($"Tarefas abertas: {abertas}");
        Console.WriteLine($"Tarefas fechadas: {fechadas}");
        Console.WriteLine($"Tarefas canceladas: {canceladas}");
        Console.WriteLine($"% concluídas em relação às abertas: {perc:F2}%");
    }

    static Tarefa? SelecionarTarefa(Projeto p)
    {
        if (!p.tarefas.Any())
        {
            Console.WriteLine("Sem tarefas.");
            return null;
        }
        foreach (var t in p.tarefas) Console.WriteLine($"[{t.id}] {t.titulo} - {t.status}");
        Console.Write("Id da tarefa: ");
        var s = Console.ReadLine();
        if (!int.TryParse(s, out int id)) return null;
        return p.buscarTarefa(id);
    }
}
