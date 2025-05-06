using Mechanic.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mechanic.Controllers
{
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
        public IActionResult Add([FromBody] Job job)
        {
            var existingJob = _mechanicDbContext.Jobs.Find(job.jobId);
            if (existingJob != null)
            {
                return Conflict();
            }

            _mechanicDbContext.Jobs.Add(job);
            _mechanicDbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete("{jobId}")]
        public IActionResult Delete(string jobId)
        {
            var existingJob = _mechanicDbContext.Jobs.Find(jobId);

            if (existingJob is null)
            {
                return NotFound();
            }

            _mechanicDbContext.Jobs.Remove(existingJob);
            _mechanicDbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Job>> GetAll()
        {
            var job = _mechanicDbContext.Jobs.ToList();
            return Ok(job);
        }

        [HttpGet("{jobId}")]
        public ActionResult<Job> Get(string jobId)
        {
            var existingJob = _mechanicDbContext.Jobs.Find(jobId);

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

            var oldJob = _mechanicDbContext.Jobs.Find(jobId);

            if (oldJob is null)
            {
                return NotFound();
            }

            oldJob.licensePlate = job.licensePlate;
            oldJob.manufacturingYear = job.manufacturingYear;
            oldJob.description = job.description;
            oldJob.severity = job.severity;
            oldJob.status = job.status;
            oldJob.workCategory = job.workCategory;

            _mechanicDbContext.Jobs.Update(oldJob);
            _mechanicDbContext.SaveChanges();

            return Ok();
        }


        [HttpGet("{jobId}/estimate")]
        public ActionResult<double> GetEstimatedHours(string jobId)
        {
            var existingJob = _mechanicDbContext.Jobs.Find(jobId);
            if (existingJob is null)
            {
                return NotFound();
            }

            var estimate = JobHelper.CalculateEstimatedHours(existingJob);

            return Ok(estimate);

        }

    }
}
