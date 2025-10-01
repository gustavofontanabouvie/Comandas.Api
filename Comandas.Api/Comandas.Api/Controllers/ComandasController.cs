using Comandas.Api.Database;
using Comandas.Api.DTOs.Comanda;
using Comandas.Application.Interfaces;
using Comandas.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ComandasController : ControllerBase
{

    private readonly IComandaService _comandaService;

    private readonly ILogger _logger;

    public ComandasController(IComandaService comandaService, ILogger<ComandasController> logger)
    {
        _comandaService = comandaService;
        _logger = logger;
    }


    [SwaggerOperation(summary: "Retorno de uma lista com todas as Comandas cadastradas")]
    [SwaggerResponse(200, "Retorna a lista das Comandas")]
    [SwaggerResponse(204, "Nenhum item encontrado")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetComandas()
    {
        var comandas = await _comandaService.GetComandas();

        if (comandas.IsNullOrEmpty())
            return NoContent();

        return Ok(comandas);
    }

    [SwaggerOperation(summary: "Retorna uma Comanda", description: "Retorno de uma comanda pelo seu ID no banco de dados, retorna também os seus CardápioItens")]
    [SwaggerResponse(404, "Comanda não encontrada")]
    [SwaggerResponse(200, "Comanda encontrada com sucesso")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ComandaByIdDto>> GetComanda(int id)
    {

        var comanda = await _comandaService.GetComandaById(id);

        if (comanda == null)
        {
            return NotFound();
        }
        return Ok(comanda);
    }


    [SwaggerOperation(summary: "Adiciona uma comanda ao banco de dados")]
    [SwaggerResponse(201, "Comanda cadastrada com sucesso")]
    [SwaggerResponse(422, "Mesa selecionada já está ocupada")]
    [HttpPost]
    public async Task<ActionResult<ComandaCreateResponseDto>> PostComanda(ComandaCreateDto comandaDto, CancellationToken cancellationToken)
    {
        var comandaResponseDto = await _comandaService.CreateComanda(comandaDto, cancellationToken);

        return CreatedAtAction("GetComanda", new { id = comandaResponseDto.id }, comandaResponseDto);
    }


    //[SwaggerOperation(summary: "Edita uma Comanda", description: "Verifica se os dados a editar são iguais e edita uma Comanda no banco de dados")]
    //[SwaggerResponse(201, "Comanda editada com sucesso")]
    //[SwaggerResponse(404, "Comanda não encontrada")]
    //[HttpPut("{id}")]
    //public async Task<ActionResult<ComandaUpdateResponseDto>> PutComanda(int id, ComandaUpdateDto updateDto, CancellationToken cancellationToken)
    //{
    //    var comanda = await _dbContext.Comandas.AsNoTracking()
    //        .Where(c => c.Id == id)
    //        .FirstOrDefaultAsync();

    //    if (comanda == null)
    //        return NotFound();

    //    if (!updateDto.nomeCliente.Equals(comanda.NomeCliente))
    //        comanda.NomeCliente = updateDto.nomeCliente;

    //    if (updateDto.numeroMesa != comanda.NumeroMesa)
    //        comanda.NumeroMesa = updateDto.numeroMesa;


    //    _dbContext.Comandas.Update(comanda);

    //    foreach (int item in updateDto.cardapioItens)
    //    {
    //        var comandaItem = new ComandaItem
    //        {
    //            CardapioItemId = item,
    //            Comanda = comanda
    //        };

    //        await _dbContext.ComandaItens.AddAsync(comandaItem);

    //        var cardapioItem = await _dbContext.CardapioItens
    //            .Where(ci => ci.Id == item)
    //            .FirstOrDefaultAsync();

    //        if (cardapioItem != null && cardapioItem.PossuiPreparo)
    //        {
    //            PedidoCozinha pedidoCozinha = new()
    //            {
    //                Comanda = comanda,
    //                Situacao = 1,
    //                PedidoCozinhaItens = new List<PedidoCozinhaItem>()
    //                {
    //                   new PedidoCozinhaItem()
    //                   {
    //                       ComandaItem = comandaItem
    //                   }
    //                }
    //            };

    //            await _dbContext.PedidosCozinha.AddAsync(pedidoCozinha);
    //        }
    //    }

    //    await _dbContext.SaveChangesAsync(cancellationToken);

    //    var updateResponse = new ComandaUpdateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

    //    return Ok(updateResponse);
    //}


    //[SwaggerOperation(summary: "Deleta uma comanda", description: "Deleta uma comanda baseada no ID")]
    //[SwaggerResponse(204, "Comanda deletada com sucesso")]
    //[SwaggerResponse(404, "Comanda não encontrada")]
    //[HttpDelete("{id}")]
    //public async Task<ActionResult> DeleteComanda(int id, CancellationToken cancellationToken)
    //{
    //    var comanda = await _dbContext.Comandas.AsNoTracking()
    //        .Where(c => c.Id == id)
    //        .FirstOrDefaultAsync();

    //    if (comanda == null)
    //        return NotFound();

    //    _dbContext.Comandas.Remove(comanda);
    //    await _dbContext.SaveChangesAsync(cancellationToken);

    //    return NoContent();
    //}

}
