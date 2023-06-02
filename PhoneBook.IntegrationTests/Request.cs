using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace PhoneBook.IntegrationTests
{
	public class Request
	{
		private readonly HttpClient client;

		public Request(HttpClient client)
		{
			this.client = client;
		}
		private StringContent StringifyRequest(object request) =>
			new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

		public async Task<T> Post<T>(string route, object request)
		{
			var requestContent = StringifyRequest(request);

			using (var postResponse = await client.PostAsync($"{route}", requestContent))
			{
				if (!postResponse.IsSuccessStatusCode) return default(T);
				var postResponseContent = await postResponse.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(postResponseContent);
			}
		}

		public async Task<HttpResponseMessage> Post(string route, object request)
		{
			var requestContent = StringifyRequest(request);

			return await client.PostAsync($"{route}", requestContent);
		}

		public async Task<HttpResponseMessage> Get(string route)
		{
			return await client.GetAsync($"{route}");
		}

		public async Task<T> Get<T>(string route)
		{
			using (var getResponse = await client.GetAsync($"{route}"))
			{
				Assert.True(getResponse.IsSuccessStatusCode);
				var getResponseContent = await getResponse.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(getResponseContent);
			}
		}

		public async Task<HttpResponseMessage> Delete(string route)
		{
			return await client.DeleteAsync($"{route}");
		}
	}
}
