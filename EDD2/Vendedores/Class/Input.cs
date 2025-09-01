// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendedores.Class
{
    public static class Input
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out int val)) return val;
                Console.WriteLine("Entrada inválida. Digite um número inteiro.");
            }
        }

        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (s == null) { Console.WriteLine("Entrada inválida."); continue; }
                s = s.Replace(',', '.').Trim();
                if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double val)) return val;
                Console.WriteLine("Entrada inválida. Digite um número (ex: 1234.56).");
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
