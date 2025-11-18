
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ProjetoAcesso
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Ambiente> Ambientes { get; set; } = new();

        public bool ConcederPermissao(Ambiente ambiente)
        {
            var jaPossui = Ambientes.Any(a => a.Id == ambiente.Id);
            if (jaPossui) return false;
            Ambientes.Add(ambiente);
            return true;
        }

        public bool RevogarPermissao(Ambiente ambiente)
        {
            var existente = Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
            if (existente == null) return false;
            Ambientes.Remove(existente);
            return true;
        }
    }

    public class Ambiente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Queue<Log> Logs { get; set; } = new();

        public void RegistrarLog(Log log)
        {
            if (Logs.Count >= 100)
                Logs.Dequeue();

            Logs.Enqueue(log);
        }
    }

    public class Log
    {
        public DateTime DtAcesso { get; set; }
        public Usuario Usuario { get; set; }
        public bool TipoAcesso { get; set; }
    }

    public class Cadastro
    {
        public List<Usuario> Usuarios { get; set; } = new();
        public List<Ambiente> Ambientes { get; set; } = new();

        public void AdicionarUsuario(Usuario usuario)
        {
            Usuarios.Add(usuario);
        }

        public bool RemoverUsuario(Usuario usuario)
        {
            var encontrado = Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (encontrado == null) return false;
            if (encontrado.Ambientes.Any()) return false;
            Usuarios.Remove(encontrado);
            return true;
        }

        public Usuario PesquisarUsuario(Usuario usuario)
        {
            return Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
        }

        public void AdicionarAmbiente(Ambiente ambiente)
        {
            Ambientes.Add(ambiente);
        }

        public bool RemoverAmbiente(Ambiente ambiente)
        {
            var encontrado = Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
            if (encontrado == null) return false;
            Ambientes.Remove(encontrado);
            return true;
        }

        public Ambiente PesquisarAmbiente(Ambiente ambiente)
        {
            return Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
        }

        public void Upload()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("/mnt/data/projetoAcessoData.json", json);
        }

        public static Cadastro Download()
        {
            var caminho = "/mnt/data/projetoAcessoData.json";
            if (!File.Exists(caminho)) return new Cadastro();
            var json = File.ReadAllText(caminho);
            return JsonSerializer.Deserialize<Cadastro>(json) ?? new Cadastro();
        }
    }

    public class Program
    {
        public static void Main()
        {
            var cadastro = Cadastro.Download();
            var executando = true;

            while (executando)
            {
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar ambiente");
                Console.WriteLine("2. Consultar ambiente");
                Console.WriteLine("3. Excluir ambiente");
                Console.WriteLine("4. Cadastrar usuario");
                Console.WriteLine("5. Consultar usuario");
                Console.WriteLine("6. Excluir usuario");
                Console.WriteLine("7. Conceder permissão");
                Console.WriteLine("8. Revogar permissão");
                Console.WriteLine("9. Registrar acesso");
                Console.WriteLine("10. Consultar logs");
                Console.Write("Opção: ");

                var opcao = Console.ReadLine();

                if (opcao == "0")
                {
                    cadastro.Upload();
                    executando = false;
                }
                else
                {
                    Console.WriteLine("Função implementada como estrutura básica para demonstração.");
                }
            }
        }
    }
}
