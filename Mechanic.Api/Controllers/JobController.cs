using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic.Shared;
using Mechanic.EFcore;
using Mechanic.IServices;

namespace Mechanic.Controllers;

[ApiController]
[Route("job")]
public class JobController : Controller
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Job job)
    {
        var existingJob = await _jobService.Get(job.jobId);

        if (existingJob != null)
        {
            return Conflict();
        }

        await _jobService.Add(job);
        return Ok();
    }


    [HttpDelete("{jobId}")]
    public async Task<IActionResult> Delete(string jobId)
    {
        var existingJob = await _jobService.Get(jobId);

        if (existingJob is null)
        {
            return NotFound();
        }

        await _jobService.Delete(jobId);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetAll()
    {
        var existingClients = await _jobService.Get();
        if (existingClients == null)
        {
            return NotFound();
        }
        return Ok(existingClients);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<Job>> Get(string jobId)
    {
        var existingJob = await _jobService.Get(jobId);

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

        var oldJob = await _jobService.Get(jobId);

        if (oldJob is null)
        {
            return NotFound();
        }

        if(!isValidStageTransition(oldJob.status, job.status))
        {
            return BadRequest("Invalid stage transition");
        }


        await _jobService.Update(job);

        return Ok();
    }


    [HttpGet("{jobId}/estimate")]
    public async Task<ActionResult<double>> GetEstimatedHours(string jobId)
    {
        var existingJob = await _jobService.GetEstimatedHours(jobId);
        if (existingJob is null)
        {
            return NotFound();
        }

        return Ok(existingJob);

    }

    //Calculates if the work stage transition is right
    //For example.:
    //WRONG TRANSITION: Under_work -> Started (2 -> 1)
    // 1 >= 2 and 1 - 2 <=  1 both returns false
    //-------------------------------------------------------
    //RIGHT TRANSITION: Started -> Under_work(1 -> 2)
    // 2 >= 1 and 2 -1 <= 1 both return true
    private bool isValidStageTransition(workStage current, workStage next)
    {
        return (int) next >= (int) current && (int) next - (int)current <=1;
    }
}
