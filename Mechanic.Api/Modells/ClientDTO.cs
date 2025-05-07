using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mechanic.Api.Modells
{
    public class ClientDTO
    {
        public string Name { get; set; }


        public string Address { get; set; }

        public string Email { get; set; }
    }

    public class ClientDetailDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
