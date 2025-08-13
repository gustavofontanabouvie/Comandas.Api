using Comandas.Api.Database;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MesasController : ControllerBase
{
    private readonly ComandasDbContext _dbContext;

    public MesasController(ComandasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mesa>>> GetMesas()
    {
        return await _dbContext.Mesas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Mesa>> GetMesa(int id)
    {
        var mesa = await _dbContext.Mesas.FindAsync(id);
        if (mesa == null)
        {
            return NotFound();
        }
        return mesa;
    }

    [HttpPost]
    public async Task<ActionResult<Mesa>> PostMesa(Mesa mesa)
    {
        _dbContext.Mesas.Add(mesa);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetMesa", new { id = mesa.Id }, mesa);
    }

}
