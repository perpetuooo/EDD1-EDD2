// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System.Collections.Generic;

namespace TransportePilha.Models
{
    public class Jornada
    {
        public bool Iniciada { get; set; }
        public List<Garagem> Garagens { get; set; } = new();
        public Frota Frota { get; set; } = new();
        public List<Viagem> Viagens { get; set; } = new();
    }
}
