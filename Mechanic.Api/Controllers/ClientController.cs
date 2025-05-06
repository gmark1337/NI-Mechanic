using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mechanic.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly MechanicDbContext _mechanicDbContext;

        public ClientController(MechanicDbContext mechanicDbContext)
        {
            _mechanicDbContext = mechanicDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Client client)
        {
            var existingClient = await _mechanicDbContext.Clients.FindAsync(client.Id);

            if (existingClient != null)
            {
                return Conflict();
            }

            _mechanicDbContext.Clients.Add(client);
            await _mechanicDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingClient = await _mechanicDbContext.Clients.FindAsync(id);

            if (existingClient is null)
            {
                return NotFound();
            }

            _mechanicDbContext.Clients.Remove(existingClient);
            await _mechanicDbContext.SaveChangesAsync();

            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAll()
        {
            var client = await _mechanicDbContext.Clients.ToListAsync();
            return Ok(client);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(string id)
        {
            var client = await _mechanicDbContext.Clients.FindAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            var oldClient = await _mechanicDbContext.Clients.FindAsync(id);


            if (oldClient is null)
            {
                return NotFound();
            }

            oldClient.Name = client.Name;
            oldClient.Address = client.Address;
            oldClient.Email = client.Email;

            _mechanicDbContext.Clients.Update(oldClient);
            await _mechanicDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
