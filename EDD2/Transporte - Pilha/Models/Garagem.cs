// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System.Collections.Generic;

namespace TransportePilha.Models
{
    public class Garagem
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Queue<Veiculo> Fila { get; set; } = new();
        public bool Bloqueada { get; set; }
    }
}
