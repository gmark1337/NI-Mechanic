public class ClientService : IClientService
{
    private readonly List<Client> _client;
    private readonly ILogger<ClientService> _logger;

    public ClientService(ILogger<ClientService> logger)
    {
        _client = [];
        _logger = logger;
    }
    public void Add(Client client)
    {
        _client.Add(client);
        _logger.LogInformation("Client added: {@Client}", client);
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

        _logger.LogInformation($"Client updated: {client.Name}");
    }
}

