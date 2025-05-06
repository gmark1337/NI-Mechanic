using Microsoft.AspNetCore.Identity;
using System;

public class ClientService : IClientService
{
    private readonly List<Client> _client;

    public ClientService()
    {
        _client = [];
    }
    public void Add(Client client)
    {
        _client.Add(client);
    }

    public void Delete(string id)
    {
        _client.RemoveAll(x => x.Id == id);
    }

    public List<Client> Get()
    {
        return _client;
    }

    public Client Get(string id)
    {
        return _client.Find(x => x.Id == id);
    }

    public void Update(Client client)
    {
        var oldClient = Get(client.Id);

        oldClient.Name = client.Name;
        oldClient.Address = client.Address;
        oldClient.Email = client.Email;
    }
}

