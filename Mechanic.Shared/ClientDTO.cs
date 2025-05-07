using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mechanic.Shared
{
    public class ClientDTO
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

    }
}
