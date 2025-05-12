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
        [RegularExpression(@"^[A-Z]{3}-\d{3}",ErrorMessage ="The license plate contains 3 capital letter and 3 numbers! For example: ASD-782")]
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

        //Ignores this attribute when sending Job to the UI because it causes Rendering issues
        [JsonIgnore]
        public  Client Client { get; set; }

        public double estimatedHours => JobHelper.CalculateEstimatedHours(this);

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum workCategory
    {
        Karosszéria,
        Motor,
        Futómű,
        Fékberendezés
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum workStage
    {
        Felvett_Munka = 0,
        Elvégzés_alatt = 1,
        Befejezett = 2
    }
}
