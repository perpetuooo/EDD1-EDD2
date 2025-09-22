// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaConsole
{
    public class Contato
    {
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public Data DtNasc { get; set; } = new Data();
        private readonly List<Telefone> telefones = new List<Telefone>();
        public IReadOnlyList<Telefone> Telefones => telefones.AsReadOnly();

        public Contato() { }

        public Contato(string nome, string email, Data dtNasc)
        {
            Nome = nome ?? string.Empty;
            Email = email ?? string.Empty;
            DtNasc = dtNasc ?? new Data();
        }

        public int GetIdade()
        {
            try
            {
                DateTime nascimento = DtNasc.ParaDateTime();
                var hoje = DateTime.Today;
                int idade = hoje.Year - nascimento.Year;
                if (nascimento > hoje.AddYears(-idade)) idade--;
                return idade;
            }
            catch
            {
                return 0;
            }
        }

        public void AdicionarTelefone(Telefone t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            if (t.Principal)
            {
                foreach (var tel in telefones)
                {
                    tel.Principal = false;
                }
            }
            else
            {
                if (!telefones.Any())
                {
                    t.Principal = true;
                }
            }

            telefones.Add(t);
        }

        public string GetTelefonePrincipal()
        {
            var principal = telefones.FirstOrDefault(t => t.Principal);
            return principal != null ? principal.Numero : "Nenhum";
        }

        public override string ToString()
        {
            var telefonesTexto = telefones.Any()
                ? string.Join(Environment.NewLine + "    ", telefones.Select(t => t.ToString()))
                : "Nenhum telefone cadastrado";

            return
$@"Nome: {Nome}
Email: {Email}
Data de nascimento: {DtNasc} (Idade: {GetIdade()} anos)
Telefone principal: {GetTelefonePrincipal()}
Telefones:
    {telefonesTexto}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Contato outro)
            {
                return string.Equals(Email?.Trim(), outro.Email?.Trim(), StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Email ?? string.Empty).ToLowerInvariant().GetHashCode();
        }
    }
}
