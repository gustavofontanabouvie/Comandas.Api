using Comandas.Api.Database;
using Comandas.Api.DTOs.Mesa;
using Comandas.Application.Interfaces;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;

namespace Comandas.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MesasController : ControllerBase
{
    private readonly IMesaService _mesaService;
    private readonly ILogger<MesasController> _logger;
    public MesasController(IMesaService mesaService, ILogger<MesasController> logger)
    {
        _logger = logger;
        _mesaService = mesaService;
    }


    //[SwaggerOperation(summary: "Retorno de uma lista com todas as Mesas cadastradas")]
    //[SwaggerResponse(200, "Retorna a lista das Mesas")]
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Mesa>>> GetMesas()
    //{
    //    return await _dbContext.Mesas.ToListAsync();
    //}


    [SwaggerOperation(summary: "Retorna uma Mesa", description: "Retorna um Mesa baseado em um ID")]
    [SwaggerResponse(404, "Mesa não encontrado")]
    [SwaggerResponse(200, "Mesa encontrado com sucesso")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MesaByIdDto>> GetMesa(int id)
    {
        var mesa = await _mesaService.GetMesaById(id);

        if (mesa == null)
        {
            return NotFound();
        }

        return Ok(mesa);
    }


    [SwaggerOperation(summary: "Cria uma Mesa")]
    [SwaggerResponse(201, "Mesa criada com sucesso")]
    [SwaggerResponse(422, "Ja possui uma mesa com essa numeração")]
    [HttpPost]
    public async Task<ActionResult<MesaResponseDto>> PostMesa(MesaCreateDto mesaDto, CancellationToken cancellationToken)
    {
        var retorno = await _mesaService.PostMesa(mesaDto, cancellationToken);

        if (retorno is null)
        {
            return UnprocessableEntity();
        }

        return CreatedAtAction("GetMesa", new { id = retorno.id }, retorno);
    }

}
