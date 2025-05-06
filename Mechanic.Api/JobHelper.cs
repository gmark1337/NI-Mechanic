using Mechanic.Api.Modells;

namespace Mechanic
{
    public static class JobHelper
    {
        public static double CalculateEstimatedHours(Job job)
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
