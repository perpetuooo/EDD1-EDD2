// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;

namespace TransportePilha.Models
{
    public class Viagem
    {
        public int Id { get; set; }
        public Veiculo Veiculo { get; set; }
        public Garagem Origem { get; set; }
        public Garagem Destino { get; set; }
        public int Pass { get; set; }
        public DateTime HoraPartida { get; set; }
        public DateTime? HoraChegada { get; set; }
    }
}
