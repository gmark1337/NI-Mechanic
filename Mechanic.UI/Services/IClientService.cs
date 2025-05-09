using Mechanic.Shared;

namespace Mechanic.UI.Services;

public interface IClientService
{
    Task<List<Client>> GetClientsAsync();
    Task<Client> GetClientAsync(string id);

    Task AddClientAsync(Client client);

    Task UpdateClientAsync(string id, Client client);

    Task DeleteClientAsync(string id);

}
