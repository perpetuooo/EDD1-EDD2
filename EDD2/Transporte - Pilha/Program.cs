// Pedro H Perpétuo CB3021688       Igor Benites CB3021734

using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using TransportePilha.Models;

namespace TransportePilha
{
	class Program
	{
		static void Main()
		{
			var caminho = "transporteData.json";
			Jornada j;
            
			if (File.Exists(caminho))
				j = JsonSerializer.Deserialize<Jornada>(File.ReadAllText(caminho)) ?? new Jornada();
			else
				j = new Jornada();

			var exec = true;
			while (exec)
			{
				Console.WriteLine("0. Finalizar");
				Console.WriteLine("1. Cadastrar veículo");
				Console.WriteLine("2. Cadastrar garagem");
				Console.WriteLine("3. Iniciar jornada");
				Console.WriteLine("4. Encerrar jornada");
				Console.WriteLine("5. Liberar viagem");
				Console.WriteLine("6. Listar veículos em garagem");
				Console.WriteLine("7. Quantidade de viagens entre origem e destino");
				Console.WriteLine("8. Listar viagens entre origem e destino");
				Console.WriteLine("9. Quantidade de passageiros entre origem e destino");
				Console.Write("Opção: ");
                
				var opc = Console.ReadLine();
				if (opc == "0")
				{
					Save(j, caminho);
					exec = false;
				}
				else if (opc == "1")
				{
					if (j.Iniciada)
					{
						Console.WriteLine("Jornada iniciada");
						continue;
					}
					Console.Write("Id: ");
					var s1 = Console.ReadLine();
					Console.Write("Cap: ");
					var s2 = Console.ReadLine();
					try
					{
						int id = int.Parse(s1);
						int cap = int.Parse(s2);
						j.Frota.Veiculos.Add(new Veiculo { Id = id, Cap = cap });
						Save(j, caminho);
					}
					catch
					{
						Console.WriteLine("Entrada inválida");
					}
				}
				else if (opc == "2")
				{
					if (j.Iniciada)
					{
						Console.WriteLine("Jornada iniciada");
						continue;
					}
					Console.Write("Id: ");
					var s1 = Console.ReadLine();
					Console.Write("Nome: ");
					var nome = Console.ReadLine();
					try
					{
						int id = int.Parse(s1);
						j.Garagens.Add(new Garagem { Id = id, Nome = nome });
						Save(j, caminho);
					}
					catch
					{
						Console.WriteLine("Entrada inválida");
					}
				}
				else if (opc == "3")
				{
					if (j.Iniciada)
					{
						Console.WriteLine("Jornada já iniciada");
						continue;
					}
					if (j.Garagens.Count == 0 || j.Frota.Veiculos.Count == 0)
					{
						Console.WriteLine("Sem garagens ou sem veículos");
						continue;
					}
					for (int i = 0; i < j.Frota.Veiculos.Count; i++)
					{
						var v = j.Frota.Veiculos[i];
						j.Garagens[i % j.Garagens.Count].Fila.Enqueue(v);
					}
					j.Iniciada = true;
					Save(j, caminho);
				}
				else if (opc == "4")
				{
					if (!j.Iniciada)
					{
						Console.WriteLine("Jornada não iniciada");
						continue;
					}
					foreach (var v in j.Frota.Veiculos)
						Console.WriteLine($"Veículo {v.Id}: {v.TotalPass} passageiros");
					j.Viagens.Clear();
					foreach (var v in j.Frota.Veiculos)
						v.TotalPass = 0;
					j.Iniciada = false;
					Save(j, caminho);
				}
				else if (opc == "5")
				{
					if (!j.Iniciada)
					{
						Console.WriteLine("Jornada não iniciada");
						continue;
					}
					Console.Write("Origem id: ");
					var s1 = Console.ReadLine();
					Console.Write("Destino id: ");
					var s2 = Console.ReadLine();
					Console.Write("Passageiros: ");
					var s3 = Console.ReadLine();
					try
					{
						int o = int.Parse(s1);
						int d = int.Parse(s2);
						int p = int.Parse(s3);
						Lib(j, o, d, p);
						Save(j, caminho);
					}
					catch
					{
						Console.WriteLine("Entrada inválida");
					}
				}
				else if (opc == "6")
				{
					Console.Write("Garagem id: ");
					var s1 = Console.ReadLine();
					try
					{
						int id = int.Parse(s1);
						ListGar(j, id);
					}
					catch { Console.WriteLine("Entrada inválida"); }
				}
				else if (opc == "7")
				{
					Console.Write("Origem id: ");
					var s1 = Console.ReadLine();
					Console.Write("Destino id: ");
					var s2 = Console.ReadLine();
					try
					{
						int o = int.Parse(s1);
						int d = int.Parse(s2);
						CountViag(j, o, d);

					}
					catch { Console.WriteLine("Entrada inválida"); }
				}
				else if (opc == "8")
				{
					Console.Write("Origem id: ");
					var s1 = Console.ReadLine();
					Console.Write("Destino id: ");
					var s2 = Console.ReadLine();

					try
					{
						int o = int.Parse(s1);
						int d = int.Parse(s2);
						ListViag(j, o, d);

					}
					catch { Console.WriteLine("Entrada inválida"); }
				}
				else if (opc == "9")
				{
					Console.Write("Origem id: ");
					var s1 = Console.ReadLine();
					Console.Write("Destino id: ");
					var s2 = Console.ReadLine();

					try
					{
						int o = int.Parse(s1);
						int d = int.Parse(s2);

						CountPass(j, o, d);
					}
					catch { Console.WriteLine("Entrada inválida"); }
				}
				else
				{
					Console.WriteLine("Opção inválida");
				}
			}
		}

		static void Save(Jornada j, string caminho)
		{
			try
			{
				var json = JsonSerializer.Serialize(j, new JsonSerializerOptions { WriteIndented = true });
				var tmp = caminho + ".tmp";

				File.WriteAllText(tmp, json);
				File.Copy(tmp, caminho, true);
				File.Delete(tmp);
			}
			catch
			{
				Console.WriteLine("Erro ao salvar");
			}
		}

		static void Lib(Jornada j, int o, int d, int p)
		{
			var go = j.Garagens.FirstOrDefault(x => x.Id == o);
			var gd = j.Garagens.FirstOrDefault(x => x.Id == d);

			if (go == null || gd == null)
			{
				Console.WriteLine("Garagem não encontrada");
				return;
			}
        
			if (go.Fila == null) go.Fila = new System.Collections.Generic.Queue<Veiculo>();
			if (gd.Fila == null) gd.Fila = new System.Collections.Generic.Queue<Veiculo>();

			if (go.Bloqueada)
			{
				Console.WriteLine("Origem bloqueada");
				return;
			}

			if (go.Fila.Count == 0)
			{
				Console.WriteLine("Sem veículos na origem");
				go.Bloqueada = true;
				return;
			}
			var v = go.Fila.Dequeue();
			v.TotalPass += p;
			v.EmViagem = false;

			var vi = new Viagem { Id = j.Viagens.Count + 1, Veiculo = v, Origem = go, Destino = gd, Pass = p, HoraPartida = DateTime.Now, HoraChegada = DateTime.Now };
			j.Viagens.Add(vi);
			gd.Fila.Enqueue(v);

			if (go.Fila.Count == 0) go.Bloqueada = true;

			gd.Bloqueada = false;
			Console.WriteLine($"Viagem liberada: v{v.Id} {go.Nome}->{gd.Nome} p:{p}");
		}

		static void ListGar(Jornada j, int id)
		{
			var g = j.Garagens.FirstOrDefault(x => x.Id == id);
			if (g == null) { Console.WriteLine("Garagem não encontrada"); return; }
			if (g.Fila == null) g.Fila = new System.Collections.Generic.Queue<Veiculo>();

			var qtd = g.Fila.Count;
			var cap = g.Fila.Sum(v => v.Cap);

			Console.WriteLine($"Garagem {g.Nome} - veículos: {qtd} capacidade total: {cap}");
		}

		static void CountViag(Jornada j, int o, int d)
		{
			var c = j.Viagens.Count(x => x.Origem != null && x.Destino != null && x.Origem.Id == o && x.Destino.Id == d);

			Console.WriteLine($"Viagens: {c}");
		}

		static void ListViag(Jornada j, int o, int d)
		{
			var list = j.Viagens.Where(x => x.Origem != null && x.Destino != null && x.Origem.Id == o && x.Destino.Id == d).ToList();
			if (!list.Any()) { Console.WriteLine("Sem viagens"); return; }

			foreach (var v in list)
				Console.WriteLine($"#{v.Id} v{v.Veiculo.Id} p:{v.Pass} {v.HoraPartida}");
		}

		static void CountPass(Jornada j, int o, int d)
		{
			var s = j.Viagens.Where(x => x.Origem != null && x.Destino != null && x.Origem.Id == o && x.Destino.Id == d).Sum(x => x.Pass);

			Console.WriteLine($"Passageiros: {s}");
		}
	}
}
