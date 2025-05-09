using Mechanic.Shared;
using System.Net.Http.Json;

namespace Mechanic.UI.Services;

public class JobService : IJobService
{
    private readonly HttpClient _httpClient;

    public JobService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddJobAsync(Job job)
    {
         await _httpClient.PostAsJsonAsync("job",job);
    }

    public async Task DeleteJobAsync(string jobId)
    {
        await _httpClient.DeleteAsync($"job/{jobId}");
    }

    public async Task<Job> GetJobAsync(string jobId)
    {
        return await _httpClient.GetFromJsonAsync<Job>($"job/{jobId}");
    }

    public async Task<List<Job>> GetJobsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Job>>("job");
    }

    public async Task UpdateJobAsync(string jobId, Job job)
    {
        await _httpClient.PutAsJsonAsync($"job/{jobId}", job);
    }
}
