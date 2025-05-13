using Mechanic.Shared;
using Mechanic.IServices;
using Mechanic.EFcore;
using Microsoft.EntityFrameworkCore;
public class ClientService : IClientService
{
    private readonly MechanicDbContext _mechanicDbContext;
    private readonly ILogger<ClientService> _logger;

    public ClientService(MechanicDbContext mechanicDb,ILogger<ClientService> logger)
    {
        _mechanicDbContext = mechanicDb;
        _logger = logger;
    }
    public async Task Add(Client client)
    {
        _mechanicDbContext.Add(client);
        await _mechanicDbContext.SaveChangesAsync();
        _logger.LogInformation("Client added: {@Client}", client);
    }

    public async Task Delete(string id)
    {
        var existingClient = await _mechanicDbContext
            .Clients.Include(j => j.jobs)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingClient != null)
        {
            _mechanicDbContext.Clients.Remove(existingClient);
            await _mechanicDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Client>> Get()
    {
        return  await _mechanicDbContext.Clients
            .Include(c => c.jobs)
            .ToListAsync();
    }

    public async Task<Client> Get(string id)
    {
        return await _mechanicDbContext.Clients
            .Include(c => c.jobs)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Update(Client client)
    {

        //Sends the client with the collection of Jobs assigned to a Client when fetched
        var oldClient = await _mechanicDbContext
            .Clients.Include(c => c.jobs)
            .FirstOrDefaultAsync(c => c.Id == client.Id);


        if (oldClient is null)
        {
            return ;
        }   

        oldClient.Name = client.Name;
        oldClient.Address = client.Address;
        oldClient.Email = client.Email;

        //updates the Collection of jobs assigned to a client
        foreach (var updatedJob in client.jobs)
        {
            var existingJob = oldClient.jobs.FirstOrDefault(j => j.jobId == updatedJob.jobId);
            if (existingJob != null)
            {
                existingJob.licensePlate = updatedJob.licensePlate;
                existingJob.description = updatedJob.description;
                existingJob.severity = updatedJob.severity;
                existingJob.status = updatedJob.status;
                existingJob.manufacturingYear = updatedJob.manufacturingYear;
                existingJob.workCategory = updatedJob.workCategory;
            }
            else
            {
                oldClient.jobs.Add(updatedJob);
            }
        }


        _mechanicDbContext.Clients.Update(oldClient);
        await _mechanicDbContext.SaveChangesAsync();
    }
}

