using Comandas.Api.Database;
using Comandas.Api.DTOs.Mesa;
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
    public async Task<ActionResult<MesaByIdDto>> GetMesa(int id)
    {
        var mesa = await _dbContext.Mesas.FindAsync(id);
        if (mesa == null)
        {
            return NotFound();
        }

        var mesaById = new MesaByIdDto(mesa.Numero, mesa.SituacaoMesa);

        return mesaById;
    }

    [HttpPost]
    public async Task<ActionResult<MesaCreateDto>> PostMesa(MesaCreateDto mesaDto)
    {
        var mesa = new Mesa
        {
            Numero = mesaDto.numero
        };

        _dbContext.Mesas.Add(mesa);
        await _dbContext.SaveChangesAsync();

        var mesaCreateDto = new MesaCreateDto(mesa.Numero);

        return CreatedAtAction("GetMesa", new { id = mesa.Id }, mesaCreateDto);
    }

}
