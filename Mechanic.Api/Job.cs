using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mechanic
{

    // • Munka állapota– Megengedett értékek: Felvett Munka-> Elvégzés alatt-> Befejezett–
    //  Az értékeket csak a nyílnak megfelelő irányba lehessen változtatni
    public class Job
    {
        //TODO: Job(jobId, id, licensePlate, ManufacturingYear, Category, Description, Severity,Status -> VALIDATION!!

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string jobId { get; set; }

        public string customerId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]{3}-\d{3}")]
        public string licensePlate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1900, 2025)]
        public int manufacturingYear { get; set; }

        [Required(AllowEmptyStrings = false)]
        public workCategory workCategory { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string description { get; set; }


        [Required(AllowEmptyStrings = false)]
        [Range(1, 10)]
        public int severity { get; set; }

        [Required(AllowEmptyStrings = false)]
        public workStage status { get; set; }

    }

    public enum workCategory
    {
        Karosszéria,
        motor,
        futómű,
        fékberendezés
    }

    public enum workStage
    {
        Felvett_Munka = 0,
        Elvégzés_alatt = 1,
        Befejezett = 2
    }
}
