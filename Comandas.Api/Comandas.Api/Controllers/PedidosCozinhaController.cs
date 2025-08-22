
using Comandas.Api.Database;
using Comandas.Api.DTOs.PedidoCozinha;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidosCozinhaController : ControllerBase
{
    private readonly ComandasDbContext _dbContext;

    public PedidosCozinhaController(ComandasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [SwaggerOperation(summary: "Retorna uma lista de todos os pedidoCozinha")]
    [SwaggerResponse(200, "Retorno da lista")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoCozinha>>> GetPedidosCozinha()
    {
        return await _dbContext.PedidosCozinha.ToListAsync();
    }

    [SwaggerOperation(summary: "Retorna um PedidoCozinha", description: "Retorna um PedidoCozinha baseado no ID")]
    [SwaggerResponse(200, "PedidoCozinha encontrado com sucesso")]
    [SwaggerResponse(404, "PedidoCozinha não encontrado")]
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoRespondeDto>> GetPedidoCozinha(int id)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.AsNoTracking()
            .Where(pc => pc.Id == id)
            .FirstOrDefaultAsync();

        if (pedidoCozinha == null)
            return NotFound();

        var pedidoResponse = new PedidoRespondeDto(pedidoCozinha.ComandaId, pedidoCozinha.Situacao);
        return Ok(pedidoResponse);
    }

    [SwaggerOperation(summary: "Avança a situação de um pedidoCozinha", description: "Edita um PedidoCozinha avançando a sua situação")]
    [SwaggerResponse(200, "PedidoCozinha editado com sucesso")]
    [SwaggerResponse(404, "PedidoCozinha não encontrado")]
    [HttpPatch("AvancarPedido/")]
    public async Task<ActionResult> AvançarPedido(int pedidoCozinhaId)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.AsNoTracking()
             .Where(pc => pc.Id == pedidoCozinhaId)
             .FirstOrDefaultAsync();

        if (pedidoCozinha == null)
            return NotFound();

        if (pedidoCozinha.Situacao == 1)
        {
            pedidoCozinha.Situacao = 2;
        }
        else if (pedidoCozinha.Situacao == 2)
        {
            pedidoCozinha.Situacao = 3;
        }
        else
        {
            pedidoCozinha.Situacao = 4;
        }

        _dbContext.PedidosCozinha.Update(pedidoCozinha);

        await _dbContext.SaveChangesAsync();

        return Ok(pedidoCozinha);
    }

    [SwaggerOperation(summary: "Volta a situação de um pedidoCozinha", description: "Edita um PedidoCozinha retornando a sua situação")]
    [SwaggerResponse(200, "PedidoCozinha editado com sucesso")]
    [SwaggerResponse(404, "PedidoCozinha não encontrado")]
    [HttpPatch("RetornarPedido/")]
    public async Task<ActionResult> RetornarPedido(int pedidoCozinhaId)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.AsNoTracking()
             .Where(pc => pc.Id == pedidoCozinhaId)
             .FirstOrDefaultAsync();

        if (pedidoCozinha == null)
            return NotFound();

        if (pedidoCozinha.Situacao == 2)
        {
            pedidoCozinha.Situacao = 1;
        }
        else if (pedidoCozinha.Situacao == 3)
        {
            pedidoCozinha.Situacao = 2;
        }
        else
        {
            return Ok(pedidoCozinha);
        }

        _dbContext.PedidosCozinha.Update(pedidoCozinha);

        await _dbContext.SaveChangesAsync();

        return Ok(pedidoCozinha);
    }

}
