using Mechanic.Shared;


namespace Mechanic.IServices;
public interface IClientService
{
    Task Add(Client client);

    Task Delete(string id);

    Task<List<Client>> Get();

    Task<Client> Get(string id);

    Task Update(Client client);
}
