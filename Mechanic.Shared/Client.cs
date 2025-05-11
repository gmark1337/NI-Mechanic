using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Mechanic.Shared;
public class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }


    [Required(AllowEmptyStrings = false)]
    public string Address { get; set; }


    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; }
    public ICollection<Job> jobs { get; set; } = new List<Job>();
}
