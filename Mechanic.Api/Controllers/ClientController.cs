using Microsoft.AspNetCore.Mvc;

namespace Mechanic.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Client client)
        {
            var existingClient = _clientService.Get(client.Id);

            if (existingClient != null)
            {
                return Conflict();
            }

            _clientService.Add(client);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existingClient = _clientService.Get(id);

            if (existingClient is null)
            {
                return NotFound();
            }

            _clientService.Delete(id);

            return Ok();

        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            var client = _clientService.Get();
            return Ok(client);
        }

        [HttpGet("{id}")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);

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

            var oldClient = _clientService.Get(id);


            if (oldClient is null)
            {
                return NotFound();
            }

            _clientService.Update(client);

            return Ok();
        }
    }
}
