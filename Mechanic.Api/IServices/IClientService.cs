using Mechanic.Shared;


namespace Mechanic.IServices;
public interface IClientService
{
    void Add(Client client);

    void Delete(string id);

    List<Client> Get();

    Client Get(string id);

    void Update(Client client);
}
