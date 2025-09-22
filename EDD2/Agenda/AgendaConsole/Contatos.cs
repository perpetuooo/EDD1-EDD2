// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AgendaConsole
{
    public class Contatos
    {
        private readonly List<Contato> agenda = new List<Contato>();
        public ReadOnlyCollection<Contato> Agenda => agenda.AsReadOnly();

        public bool Adicionar(Contato c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));

            var existe = agenda.Any(a => string.Equals(a.Email.Trim(), c.Email.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (existe) return false;

            agenda.Add(c);
            return true;
        }

        public Contato? Pesquisar(Contato c)
        {
            if (c == null) return null;

            if (!string.IsNullOrWhiteSpace(c.Email))
            {
                return agenda.FirstOrDefault(a => string.Equals(a.Email.Trim(), c.Email.Trim(), StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(c.Nome))
            {
                return agenda.FirstOrDefault(a => a.Nome.IndexOf(c.Nome.Trim(), StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            return null;
        }

        public bool Alterar(Contato c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (string.IsNullOrWhiteSpace(c.Email)) return false;

            var indice = agenda.FindIndex(a => string.Equals(a.Email.Trim(), c.Email.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (indice < 0) return false;

            agenda[indice] = c;
            return true;
        }

        public bool Remover(Contato c)
        {
            var existente = Pesquisar(c);
            if (existente == null) return false;
            return agenda.Remove(existente);
        }
    }
}
