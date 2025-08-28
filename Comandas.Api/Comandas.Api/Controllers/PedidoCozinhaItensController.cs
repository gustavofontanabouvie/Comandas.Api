
using Comandas.Api.Database;
using Comandas.Api.DTOs.PedidoCozinhaItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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


    [SwaggerOperation(summary: "Retorna uma lista de todos os pedidoCozinhaItens")]
    [SwaggerResponse(200, "Retorno da lista")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoCozinhaItem>>> GetPedidosCozinhaItem()
    {
        return await _dbContext.PedidoCozinhaItens.ToListAsync();
    }

    [SwaggerOperation(summary: "Retorna um PedidoCozinhaItem", description: "Retorna um PedidoCozinhaItem baseado no ID")]
    [SwaggerResponse(200, "PedidoCozinhaItem encontrado com sucesso")]
    [SwaggerResponse(404, "PedidoCozinhaItem não encontrado")]
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoCozinhaItemResponseDto>> GetPedidoCozinhaItens(int id)
    {
        var pedidoCozinhaItem = await _dbContext.PedidoCozinhaItens.AsNoTracking()
            .Where(pci => pci.Id == id)
            .FirstOrDefaultAsync();

        if (pedidoCozinhaItem == null)
            return NotFound();

        var responseDto = new PedidoCozinhaItemResponseDto(pedidoCozinhaItem.ComandaItemId, pedidoCozinhaItem.PedidoCozinhaId);

        return Ok(responseDto);
    }

    [SwaggerOperation(summary: "Retorna os PedidoCozinhaItens pendentes", description: "Retorna uma lista de todos os pedidos que estão pendentes, retorna o NumeroMesa,NomeCliente,Item e ID do pedidoCozinha")]
    [SwaggerResponse(200, "Lista retornada com sucesso")]
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
                             select new PedidoResponseSituacaoDto
                             (

                                 comanda.NumeroMesa,
                                 comanda.NomeCliente,
                                 cardapioItem.Titulo,
                                 pedidoCozinha.Id
                             )
                                      ).ToListAsync();

        return Ok(pedidos);
    }


    [SwaggerOperation(summary: "Retorna os PedidoCozinhaItens em andamento", description: "Retorna uma lista de todos os pedidos que estão em andamento, retorna o NumeroMesa,NomeCliente,Item e ID do pedidoCozinha")]
    [SwaggerResponse(200, "Lista de pedidos em andamento")]
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
            var pedidoResponseDto = new PedidoResponseSituacaoDto(pedido.NumeroMesa, pedido.NomeCliente, pedido.Titulo, pedido.Id);
            listaDto.Add(pedidoResponseDto);
        }

        return Ok(listaDto);
    }


    [SwaggerOperation(summary: "Retorna os PedidoCozinhaItens finalizados", description: "Retorna uma lista de todos os pedidos que estão finalizados, retorna o NumeroMesa,NomeCliente,Item e ID do pedidoCozinha")]
    [SwaggerResponse(200, "Lista retornada com sucesso")]
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
            var pedidoResponseDto = new PedidoResponseSituacaoDto(pedido.NumeroMesa, pedido.NomeCliente, pedido.Titulo, pedido.Id);
            listaDto.Add(pedidoResponseDto);
        }

        return Ok(listaDto);
    }
}
