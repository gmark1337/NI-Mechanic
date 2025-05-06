using Mechanic.Api.Modells;

namespace Mechanic.Api.IServices
{
    public interface IJobService
    {
        void Add(Job job);

        void Delete(string jobId);

        List<Job> Get();

        Job Get(string jobId);

        void Update(Job job);

        double GetEstimatedHours(string jobId);

        double CalculateEstimatedHours(Job job);



    }
}
