using System;
using System.ComponentModel.DataAnnotations;

public class Client
{
    public string Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Address { get; set; }

    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; }
}
