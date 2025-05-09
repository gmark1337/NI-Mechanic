using System.Net.Http.Json;

namespace Mechanic.UI.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task AddClientAsync(Client client)
        {
            await _httpClient.PostAsJsonAsync("client", client);  
        }

        public async Task DeleteClientAsync(string id)
        {
            await _httpClient.DeleteAsync($"client/{id}");
        }

        public async Task<Client> GetClientAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Client>($"client/{id}");
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Client>>("clients");
        }

        public async Task UpdateClientAsync(string id, Client clientDTO)
        {
            await _httpClient.PutAsJsonAsync($"client/{id}", clientDTO);
        }
    }
}
