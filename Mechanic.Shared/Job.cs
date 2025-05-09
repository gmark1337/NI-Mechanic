using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Mechanic.Shared;

namespace Mechanic
{
    public class Job
    {

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


        [ForeignKey("customerId")]
        public virtual Client Client { get; set; }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum workCategory
    {
        Karosszéria,
        Motor,
        Futómű,
        Fékberendezés
    }


    public enum workStage
    {
        Felvett_Munka = 0,
        Elvégzés_alatt = 1,
        Befejezett = 2
    }
}
