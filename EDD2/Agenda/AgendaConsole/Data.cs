// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Globalization;

namespace AgendaConsole
{
    public class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public int Dia
        {
            get => dia;
            set
            {
                if (value < 1 || value > 31) throw new ArgumentOutOfRangeException(nameof(Dia));
                dia = value;
            }
        }

        public int Mes
        {
            get => mes;
            set
            {
                if (value < 1 || value > 12) throw new ArgumentOutOfRangeException(nameof(Mes));
                mes = value;
            }
        }

        public int Ano
        {
            get => ano;
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(Ano));
                ano = value;
            }
        }

        public Data() { }

        public Data(int dia, int mes, int ano)
        {
            SetData(dia, mes, ano);
        }

        public void SetData(int dia, int mes, int ano)
        {
            var texto = $"{dia:D2}/{mes:D2}/{ano:D4}";
            if (!DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _))
            {
                throw new ArgumentException("Data inválida.");
            }

            this.Dia = dia;
            this.Mes = mes;
            this.Ano = ano;
        }

        public override string ToString()
        {
            return $"{Dia:D2}/{Mes:D2}/{Ano:D4}";
        }

        public DateTime ParaDateTime()
        {
            return new DateTime(Ano, Mes, Dia);
        }
    }
}
