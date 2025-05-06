using Mechanic.Api.IServices;
using Mechanic.Api.Modells;

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
            return CalculateEstimatedHours(existingJob);
        }

        public double CalculateEstimatedHours(Job job)
        {
            double baseHours = job.workCategory switch
            {
                workCategory.Karosszéria => 3,
                workCategory.motor => 8,
                workCategory.futómű => 6,
                workCategory.fékberendezés => 4,
                _ => 0
            };

            int currentYear = DateTime.Now.Year;
            int carAge = currentYear - job.manufacturingYear;

            double lifeCycle = carAge switch
            {
                <= 5 => 0.5,
                <= 10 => 1,
                <= 20 => 1.5,
                _ => 2
            };

            double severityMultiplier = job.severity switch
            {
                <= 2 => 0.2,
                <= 4 => 0.4,
                <= 7 => 0.6,
                <= 9 => 0.8,
                _ => 1
            };

            return Math.Round(baseHours * lifeCycle * severityMultiplier, 2);
        }


    }
}

