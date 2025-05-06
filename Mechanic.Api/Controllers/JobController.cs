using Mechanic;
using Microsoft.AspNetCore.Mvc;

namespace Mechanic.Controllers
{
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
        public IActionResult Add([FromBody] Job job)
        {
            var existingJob = _jobService.Get(job.jobId);
            if (existingJob != null)
            {
                return Conflict();
            }

            _jobService.Add(job);

            return Ok();
        }


        [HttpDelete("{jobId}")]
        public IActionResult Delete(string jobId)
        {
            var existingJob = _jobService.Get(jobId);

            if (existingJob is null)
            {
                return NotFound();
            }

            _jobService.Delete(jobId);

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Job>> GetAll()
        {
            var job = _jobService.Get();
            return Ok(job);
        }

        [HttpGet("{jobId}")]
        public ActionResult<Job> Get(string jobId)
        {
            var existingJob = _jobService.Get(jobId);

            if (jobId is null)
            {
                return NotFound();
            }

            return Ok(existingJob);
        }


        [HttpPut("{jobId}")]
        public IActionResult Update(string jobId, [FromBody] Job job)
        {
            if (jobId != job.jobId)
            {
                return BadRequest();
            }

            var oldJob = _jobService.Get(jobId);

            if (oldJob is null)
            {
                return NotFound();
            }

            _jobService.Update(job);

            return Ok();
        }
    }
}
