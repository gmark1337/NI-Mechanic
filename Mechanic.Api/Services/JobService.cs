namespace Mechanic.Api.Services;

using Mechanic.EFcore;
using Mechanic.IServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class JobService : IJobService
{
    private readonly MechanicDbContext _mechanicDbContext;

    public JobService(MechanicDbContext context)
    {
        _mechanicDbContext = context;
    }
    public async Task Add(Job job)
    {
        _mechanicDbContext.Add(job);
        await _mechanicDbContext.SaveChangesAsync();
    }

    public async Task Delete(string jobId)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(jobId);

        if (existingJob != null)
        {
            _mechanicDbContext.Jobs.Remove(existingJob);
            await _mechanicDbContext.SaveChangesAsync();
        }

        
    }

    public async Task <List<Job>> Get()
    {
        return await _mechanicDbContext.Jobs.ToListAsync();
    }

    public async Task<Job> Get(string jobId)
    {
        return await _mechanicDbContext.Jobs.FindAsync(jobId);
    }

    public async Task Update(Job job)
    {
        var oldJob = await _mechanicDbContext.Jobs.FindAsync(job.jobId);

        if (oldJob == null) {
            return;
        }

        oldJob.licensePlate = job.licensePlate;
        oldJob.manufacturingYear = job.manufacturingYear;
        oldJob.description = job.description;
        oldJob.severity = job.severity;
        oldJob.status = job.status;
        oldJob.workCategory = job.workCategory;

        _mechanicDbContext.Jobs.Update(oldJob);
        await _mechanicDbContext.SaveChangesAsync();
    }

    public async Task<double?> GetEstimatedHours(string jobId)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(jobId);
        return existingJob == null ? null : JobHelper.CalculateEstimatedHours(existingJob);

    }

}

