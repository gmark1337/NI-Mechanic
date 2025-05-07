namespace Mechanic.Api.Services
{
    public class JobService : IJobService
    {
        private readonly List<Job> _jobs;

        public JobService()
        {
            _jobs = [];
        }
        public void Add(Job job)
        {
            _jobs.Add(job);
        }

        public void Delete(string jobId)
        {
            _jobs.RemoveAll(x => x.jobId == jobId);
        }

        public List<Job> Get()
        {
            return _jobs;
        }

        public Job Get(string jobId)
        {
            return _jobs.Find(x => x.jobId == jobId);
        }

        public void Update(Job job)
        {
            var oldJob = Get(job.jobId);

            if ((int)job.status < (int)oldJob.status)
            {
                throw new InvalidOperationException("Work stage can only change forward!");
            }

            oldJob.licensePlate = job.licensePlate;
            oldJob.manufacturingYear = job.manufacturingYear;
            oldJob.description = job.description;
            oldJob.severity = job.severity;
            oldJob.status = job.status;
            oldJob.workCategory = job.workCategory;
        }

        public double GetEstimatedHours(string jobId)
        {
            var existingJob = Get(jobId);
            return JobHelper.CalculateEstimatedHours(existingJob);
        }

    }
}

