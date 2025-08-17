
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

    [HttpGet("PedidosPendentes/")]
    public async Task<ActionResult<List<PedidoResponseSituacaoDto>>> GetPedidosPendentes()
    {
        var pedidos = await (from pedidoCozinhaItem in _dbContext.PedidoCozinhaItens
                             join pedidoCozinha in _dbContext.PedidosCozinha
                               on pedidoCozinhaItem.PedidoCozinhaId equals pedidoCozinha.Id
                             join comanda in _dbContext.Comandas
                               on pedidoCozinha.ComandaId equals comanda.Id
                             join comandaItem in _dbContext.ComandaItens
                               on comanda.Id equals comandaItem.ComandaId
                             join cardapioItem in _dbContext.CardapioItens
                               on comandaItem.CardapioItemId equals cardapioItem.Id
                             where pedidoCozinha.Situacao == 1
                             && comanda.SituacaoComanda
                             && pedidoCozinhaItem.ComandaItemId == comandaItem.Id
                             select new
                             {
                                 comanda.NumeroMesa,
                                 comanda.NomeCliente,
                                 cardapioItem.Titulo,
                                 pedidoCozinha.Id
                             }
                                      ).ToListAsync();

        List<PedidoResponseSituacaoDto> listaDto = new();

        foreach (var pedido in pedidos)
        {
            var pedidoResponseDto = new PedidoResponseSituacaoDto(pedido.NumeroMesa, pedido.NomeCliente, pedido.Titulo);
            listaDto.Add(pedidoResponseDto);
        }

        return listaDto;
    }

    [HttpGet("PedidosEmAndamento/")]
    public async Task<ActionResult<List<PedidoResponseSituacaoDto>>> GetPedidosEmAndamento(int pedidoCozinhaId)
    {
        var pedidos = await (from pedidoCozinhaItem in _dbContext.PedidoCozinhaItens
                             join pedidoCozinha in _dbContext.PedidosCozinha
                               on pedidoCozinhaItem.PedidoCozinhaId equals pedidoCozinha.Id
                             join comanda in _dbContext.Comandas
                               on pedidoCozinha.ComandaId equals comanda.Id
                             join comandaItem in _dbContext.ComandaItens
                               on comanda.Id equals comandaItem.ComandaId
                             join cardapioItem in _dbContext.CardapioItens
                               on comandaItem.CardapioItemId equals cardapioItem.Id
                             where pedidoCozinha.Situacao == 2
                             && comanda.SituacaoComanda
                             && pedidoCozinhaItem.ComandaItemId == comandaItem.Id
                             select new
                             {
                                 comanda.NumeroMesa,
                                 comanda.NomeCliente,
                                 cardapioItem.Titulo,
                                 pedidoCozinha.Id
                             }
                              ).ToListAsync();

        List<PedidoResponseSituacaoDto> listaDto = new();

        foreach (var pedido in pedidos)
        {
            var pedidoResponseDto = new PedidoResponseSituacaoDto(pedido.NumeroMesa, pedido.NomeCliente, pedido.Titulo);
            listaDto.Add(pedidoResponseDto);
        }

        return listaDto;
    }

    [HttpGet("PedidosFinalizados/")]
    public async Task<ActionResult<List<PedidoResponseSituacaoDto>>> GetPedidosFinalizados(int pedidoCozinhaId)
    {
        var pedidos = await (from pedidoCozinhaItem in _dbContext.PedidoCozinhaItens
                             join pedidoCozinha in _dbContext.PedidosCozinha
                               on pedidoCozinhaItem.PedidoCozinhaId equals pedidoCozinha.Id
                             join comanda in _dbContext.Comandas
                               on pedidoCozinha.ComandaId equals comanda.Id
                             join comandaItem in _dbContext.ComandaItens
                               on comanda.Id equals comandaItem.ComandaId
                             join cardapioItem in _dbContext.CardapioItens
                               on comandaItem.CardapioItemId equals cardapioItem.Id
                             where pedidoCozinha.Situacao == 3
                             && comanda.SituacaoComanda
                             && pedidoCozinhaItem.ComandaItemId == comandaItem.Id
                             select new
                             {
                                 comanda.NumeroMesa,
                                 comanda.NomeCliente,
                                 cardapioItem.Titulo,
                                 pedidoCozinha.Id
                             }
                              ).ToListAsync();

        List<PedidoResponseSituacaoDto> listaDto = new();

        foreach (var pedido in pedidos)
        {
            var pedidoResponseDto = new PedidoResponseSituacaoDto(pedido.NumeroMesa, pedido.NomeCliente, pedido.Titulo);
            listaDto.Add(pedidoResponseDto);
        }

        return listaDto;
    }
}
