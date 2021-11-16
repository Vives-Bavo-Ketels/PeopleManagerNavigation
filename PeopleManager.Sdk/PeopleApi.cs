using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PeopleManager.Services.Model;

namespace PeopleManager.Sdk
{
	public class PeopleApi
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public PeopleApi(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<PersonDto> Get(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleApi");
			
			var route = $"people/{id}";
			var httpResponse = await httpClient.GetAsync(route);

			httpResponse.EnsureSuccessStatusCode();

			return await httpResponse.Content.ReadFromJsonAsync<PersonDto>();
		}

		public async Task<IList<PersonDto>> Find()
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleApi");

			var route = "people";
			var httpResponse = await httpClient.GetAsync(route);

			httpResponse.EnsureSuccessStatusCode();

			return await httpResponse.Content.ReadFromJsonAsync<IList<PersonDto>>();
		}

		public async Task<PersonDto> Create(PersonDto person)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleApi");

			var route = "people";
			var httpResponse = await httpClient.PostAsJsonAsync(route, person);

			httpResponse.EnsureSuccessStatusCode();

			return await httpResponse.Content.ReadFromJsonAsync<PersonDto>();
		}

		public async Task<PersonDto> Update(int id, PersonDto person)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleApi");

			var route = $"people/{id}";
			var httpResponse = await httpClient.PutAsJsonAsync(route, person);

			httpResponse.EnsureSuccessStatusCode();

			return await httpResponse.Content.ReadFromJsonAsync<PersonDto>();
		}

		public async Task Delete(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleApi");

			var route = $"people/{id}";
			var httpResponse = await httpClient.DeleteAsync(route);

			httpResponse.EnsureSuccessStatusCode();
			
		}
	}
}
