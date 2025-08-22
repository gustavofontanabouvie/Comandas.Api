using Comandas.Api.Database;
using Comandas.Api.DTOs.Mesa;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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


    [SwaggerOperation(summary: "Retorno de uma lista com todas as Mesas cadastradas")]
    [SwaggerResponse(200, "Retorna a lista das Mesas")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mesa>>> GetMesas()
    {
        return await _dbContext.Mesas.ToListAsync();
    }


    [SwaggerOperation(summary: "Retorna uma Mesa", description: "Retorna um Mesa baseado em um ID")]
    [SwaggerResponse(404, "Mesa não encontrado")]
    [SwaggerResponse(200, "Mesa encontrado com sucesso")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MesaByIdDto>> GetMesa(int id)
    {
        var mesa = await _dbContext.Mesas.AsNoTracking()
            .Where(me => me.Id == id)
            .FirstOrDefaultAsync();

        if (mesa == null)
        {
            return NotFound();
        }

        var mesaById = new MesaByIdDto(mesa.Numero, mesa.SituacaoMesa);

        return mesaById;
    }


    [SwaggerOperation(summary: "Cria uma Mesa")]
    [SwaggerResponse(201, "Mesa criada com sucesso")]
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
