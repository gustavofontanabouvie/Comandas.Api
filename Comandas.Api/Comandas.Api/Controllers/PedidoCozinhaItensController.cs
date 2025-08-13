
using Comandas.Api.Database;
using Comandas.Api.DTOs.PedidoCozinhaItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoCozinhaItensController : ControllerBase
{
    private readonly ComandasDbContext _dbContext;

    public PedidoCozinhaItensController(ComandasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoCozinhaItem>>> GetPedidosCozinhaItem()
    {
        return await _dbContext.PedidoCozinhaItens.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoCozinhaItemResponseDto>> GetPedidoCozinhaItens(int id)
    {
        var pedidoCozinhaItem = await _dbContext.PedidoCozinhaItens.FindAsync(id);

        if (pedidoCozinhaItem == null)
            return NotFound();

        var responseDto = new PedidoCozinhaItemResponseDto(pedidoCozinhaItem.ComandaItemId, pedidoCozinhaItem.PedidoCozinhaId);

        return responseDto;
    }

}
