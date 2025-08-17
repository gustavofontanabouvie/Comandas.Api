
using Comandas.Api.Database;
using Comandas.Api.DTOs.PedidoCozinha;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoCozinha>>> GetPedidosCozinha()
    {
        return await _dbContext.PedidosCozinha.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoRespondeDto>> GetPedidoCozinha(int id)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.FindAsync(id);

        if (pedidoCozinha == null)
            return NotFound();

        var pedidoResponse = new PedidoRespondeDto(pedidoCozinha.ComandaId, pedidoCozinha.Situacao);
        return pedidoResponse;
    }


    [HttpPatch("AvancarPedido/")]
    public async Task<ActionResult> AvançarPedido(int pedidoCozinhaId)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.FindAsync(pedidoCozinhaId);

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

    [HttpPatch("RetornarPedido/")]
    public async Task<ActionResult> RetornarPedido(int pedidoCozinhaId)
    {
        var pedidoCozinha = await _dbContext.PedidosCozinha.FindAsync(pedidoCozinhaId);

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
