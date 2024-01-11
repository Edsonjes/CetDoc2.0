using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CetDocsApp.Utils
{
	public static class HttpClientExtensions
	{
		private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
		public  static async Task<T> ReadContentAsync<T>( this HttpResponseMessage response)
		{
			if(!response.IsSuccessStatusCode)
				throw new Exception("Erro ao obter dados da API: " + $"{response.ReasonPhrase}" );
				var dataAsString = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

		}

		public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
		{
			var dataAsString = JsonSerializer.Serialize(data);
			var content = new StringContent(dataAsString);
			content.Headers.ContentType = contentType;
			return await httpClient.PostAsync(url, content);
		}

		public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
		{
			var dataAsString = JsonSerializer.Serialize(data);
			var content = new StringContent(dataAsString);
			content.Headers.ContentType = contentType;
			return await httpClient.PostAsync(url, content);
		}

	}
}
