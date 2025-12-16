
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
	static void Main(string[] args)
	{
		try
		{
			RunAsync().GetAwaiter().GetResult();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Erro ao executar:");
			Console.WriteLine(ex.Message);
		}
        
		Console.ReadKey();
	}

	private static async Task RunAsync()
	{
		string clientId = "05eb88a940484bd08e8f2f263e56eb2b";
		// string clientSecret = "8977e54ba82a23a016db74f4504f6b2d";
		// string username = "ifspcbtadsedd@gmail.com";
		// string password = "Senha@123";
		// string passwordMd5 = ToMD5(password);
		int lockId = 17097086;
		string accessToken = "1468737f4460a3a39bc2ca5186ca1097"; // TOKEN ESTÁTICO

		using (var httpClient = new HttpClient())
		{
			// --- GERAÇÃO DINÂMICA DE TOKEN COMENTADA ---
			/*
			var tokenUrl = "https://euapi.ttlock.com/oauth2/token";
			var tokenParams = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("client_id", clientId),
				new KeyValuePair<string, string>("client_secret", clientSecret),
				new KeyValuePair<string, string>("username", username),
				new KeyValuePair<string, string>("password", passwordMd5),
				new KeyValuePair<string, string>("grant_type", "password")
			};
			var tokenContent = new FormUrlEncodedContent(tokenParams);
			tokenContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
			Console.WriteLine("Solicitando access_token...");
			string accessToken = null;
			using (var tokenResponse = await httpClient.PostAsync(tokenUrl, tokenContent))
			{
				var tokenBody = await tokenResponse.Content.ReadAsStringAsync();
				if (!tokenResponse.IsSuccessStatusCode)
				{
					throw new Exception($"Erro ao obter token: {tokenResponse.StatusCode}: {tokenBody}");
				}
				using var doc = JsonDocument.Parse(tokenBody);
				if (doc.RootElement.TryGetProperty("access_token", out var tokenElem))
				{
					accessToken = tokenElem.GetString();
				}
				else
				{
					throw new Exception("Resposta de token não contém 'access_token':\n" + tokenBody);
				}
			}
			*/

			var url = "https://euapi.ttlock.com/v3/lock/unlock";
			long date = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
			var formContent = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("clientId", clientId),
				new KeyValuePair<string, string>("accessToken", accessToken),
				new KeyValuePair<string, string>("lockId", lockId.ToString()),
				new KeyValuePair<string, string>("date", date.ToString())
			});

			using (var response = await httpClient.PostAsync(url, formContent))
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				if (!response.IsSuccessStatusCode)
				{
					throw new Exception($"HTTP error {response.StatusCode}: {responseBody}");
				}

				var ttResp = JsonSerializer.Deserialize<TtlockResponse>(responseBody);
				if (ttResp == null)
					throw new Exception("TTLock error");
				if (ttResp.errcode != 0)
					throw new Exception($"TTLock error {ttResp.errcode}: {ttResp.errmsg}");

				Console.WriteLine("Fechadura destravada com sucesso.");
			}
		}
	}
	public class TtlockResponse
	{
		public int errcode { get; set; }
		public string errmsg { get; set; }
		public string description { get; set; }
	}
}
