using Mechanic.Shared;


namespace Mechanic.UI.Services;

public interface IJobService
{
    Task<List<Job>> GetJobsAsync();

    Task<Job> GetJobAsync(string jobId);

    Task DeleteJobAsync(string jobId);

    Task AddJobAsync(Job job);

    Task UpdateJobAsync(string jobId, Job job);

}
