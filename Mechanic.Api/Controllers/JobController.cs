using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic.Shared;
using Mechanic.EFcore;

namespace Mechanic.Controllers;

[ApiController]
[Route("job")]
public class JobController : Controller
{
    private readonly MechanicDbContext _mechanicDbContext;

    public JobController(MechanicDbContext mechanicDbContext)
    {
        _mechanicDbContext = mechanicDbContext;
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Job job)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(job.jobId);
        if (existingJob != null)
        {
            return Conflict();
        }

        var existingClient = await _mechanicDbContext.Clients.FindAsync(job.customerId);
        if (existingClient == null)
        {
            return BadRequest();
        }

        _mechanicDbContext.Jobs.Add(job);
        await _mechanicDbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpDelete("{jobId}")]
    public async Task<IActionResult> Delete(string jobId)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(jobId);

        if (existingJob is null)
        {
            return NotFound();
        }

        _mechanicDbContext.Jobs.Remove(existingJob);
        await _mechanicDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetAll()
    {
        var job = await _mechanicDbContext.Jobs.ToListAsync();
        return Ok(job);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<Job>> Get(string jobId)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(jobId);

        if (existingJob is null)
        {
            return NotFound();
        }

        return Ok(existingJob);
    }


    [HttpPut("{jobId}")]
    public async Task<IActionResult> Update(string jobId, [FromBody] Job job)
    {
        if (jobId != job.jobId)
        {
            return BadRequest();
        }

        var oldJob = await _mechanicDbContext.Jobs.FindAsync(jobId);

        if (oldJob is null)
        {
            return NotFound();
        }

        if(!isValidStageTransition(oldJob.status, job.status))
        {
            return BadRequest("Invalid stage transition");
        }

        oldJob.licensePlate = job.licensePlate;
        oldJob.manufacturingYear = job.manufacturingYear;
        oldJob.description = job.description;
        oldJob.severity = job.severity;
        oldJob.status = job.status;
        oldJob.workCategory = job.workCategory;

        _mechanicDbContext.Jobs.Update(oldJob);
        await _mechanicDbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpGet("{jobId}/estimate")]
    public async Task<ActionResult<double>> GetEstimatedHours(string jobId)
    {
        var existingJob = await _mechanicDbContext.Jobs.FindAsync(jobId);
        if (existingJob is null)
        {
            return NotFound();
        }

        var estimate = JobHelper.CalculateEstimatedHours(existingJob);

        return Ok(estimate);

    }

    private bool isValidStageTransition(workStage current, workStage next)
    {
        return (int) next >= (int) current && (int) next - (int)current <=1;
    }
}
