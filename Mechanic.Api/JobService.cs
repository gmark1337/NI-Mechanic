
using Mechanic;

namespace Mechanic
{
    public class JobService : IJobService
    {
        private readonly List<Job> _job;

        public JobService()
        {
            _job = [];
        }
        public void Add(Job job)
        {
            _job.Add(job);
        }

        public void Delete(string jobId)
        {
            _job.RemoveAll(x => x.jobId == jobId);
        }

        public List<Job> Get()
        {
            return _job;
        }

        public Job Get(string jobId)
        {
            return _job.Find(x => x.jobId == jobId);
        }

        public void Update(Job job)
        {
            var oldJob = Get(job.jobId);

            oldJob.licensePlate = job.licensePlate;
            oldJob.manufactingYear = job.manufactingYear;
            oldJob.description = job.description;
            oldJob.severity = job.severity;
            oldJob.status = job.status;
        }
    }
}
