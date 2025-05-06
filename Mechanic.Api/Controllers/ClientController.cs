using Microsoft.AspNetCore.Mvc;

namespace Mechanic.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        //private readonly IClientService _clientService;
        private readonly MechanicDbContext _mechanicDbContext;

        public ClientController(MechanicDbContext mechanicDbContext)
        {
            _mechanicDbContext = mechanicDbContext;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Client client)
        {
            var existingClient = _mechanicDbContext.Clients.Find(client.Id);

            if (existingClient != null)
            {
                return Conflict();
            }

            _mechanicDbContext.Clients.Add(client);
            _mechanicDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existingClient = _mechanicDbContext.Clients.Find(id);

            if (existingClient is null)
            {
                return NotFound();
            }

            _mechanicDbContext.Clients.Remove(existingClient);
            _mechanicDbContext.SaveChanges();

            return Ok();

        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            var client = _mechanicDbContext.Clients.ToList();
            return Ok(client);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get(string id)
        {
            var client = _mechanicDbContext.Clients.Find(id);

            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            var oldClient = _mechanicDbContext.Clients.Find(id);


            if (oldClient is null)
            {
                return NotFound();
            }

            oldClient.Name = client.Name;
            oldClient.Address = client.Address;
            oldClient.Email = client.Email;

            _mechanicDbContext.Clients.Update(oldClient);
            _mechanicDbContext.SaveChanges();

            return Ok();
        }
    }
}
