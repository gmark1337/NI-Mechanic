using Mechanic;

namespace Mechanic
{
    public interface IJobService
    {
        void Add(Job job);

        void Delete(string jobId);

        List<Job> Get();

        Job Get(string jobId);

        void Update(Job job);

    }
}
