// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

namespace AgendaWinForms
{
    public class Telefone
    {
        public string Tipo { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public bool Principal { get; set; } = false;

        public Telefone() { }

        public Telefone(string tipo, string numero, bool principal = false)
        {
            Tipo = tipo ?? string.Empty;
            Numero = numero ?? string.Empty;
            Principal = principal;
        }

        public override string ToString()
        {
            return $"{Tipo}: {Numero}" + (Principal ? " (principal)" : "");
        }
    }
}
