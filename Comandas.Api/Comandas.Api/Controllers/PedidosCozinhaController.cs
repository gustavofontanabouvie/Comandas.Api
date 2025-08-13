
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
}
