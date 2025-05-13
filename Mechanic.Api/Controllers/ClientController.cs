using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic.Shared;
using Mechanic.EFcore;
using Mechanic.IServices;

namespace Mechanic.Controllers;


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
    public async Task<IActionResult> Add([FromBody] Client client)
    {
        var existingClient = await _clientService.Get(client.Id);

        if (existingClient != null)
        {
            return Conflict();
        }

        
        await _clientService.Add(client);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingClient = _clientService.Get(id);

        if (existingClient is null)
        {
            return NotFound();
        }

        await _clientService.Delete(id);

        return Ok();

    }

    [HttpGet]
    public async Task<ActionResult<List<Client>>> GetAll()
    {
        var existingClients = await _clientService.Get();

        if(existingClients is null)
        {
            return NotFound();
        }

        return Ok(existingClients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> Get(string id)
    {
        var existingClient = await _clientService.Get(id);

        if (existingClient is null)
        {
            return NotFound();
        }

        return Ok(existingClient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Client client)
    {
        if(id != client.Id)
        {
            return BadRequest();
        }

        var existingClient = _clientService.Get(id);

        if(existingClient is null)
        {
            return NotFound();
        }

        await _clientService.Update(client);
        return Ok();
    }
}
