namespace Mechanic.IServices;

public interface IJobService
{
    Task Add(Job job);

    Task Delete(string jobId);

    Task<List<Job>> Get();

    Task<Job> Get(string jobId);

    Task Update(Job job);

    Task<double?> GetEstimatedHours(string jobId);

}
