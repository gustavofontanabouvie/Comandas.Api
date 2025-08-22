using Comandas.Api.Database;
using Comandas.Api.DTOs.Comanda;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ComandasController : ControllerBase
{

    private readonly ComandasDbContext _dbContext;
    public ComandasController(ComandasDbContext context)
    {
        _dbContext = context;
    }


    [SwaggerOperation(summary: "Retorno de uma lista com todas as Comandas cadastradas")]
    [SwaggerResponse(200, "Retorna a lista das Comandas")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetComandas()
    {
        return await _dbContext.Comandas.ToListAsync();
    }

    [SwaggerOperation(summary: "Retorna uma Comanda", description: "Retorno de uma comanda pelo seu ID no banco de dados, retorna também os seus CardápioItens")]
    [SwaggerResponse(404, "Comanda não encontrada")]
    [SwaggerResponse(200, "Comanda encontrada com sucesso")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ComandaByIdDto>> GetComanda(int id)
    {
        var comanda = await _dbContext.Comandas.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ComandaDto(c.NumeroMesa, c.NomeCliente, c.SituacaoComanda))
            .TagWith(nameof(GetComanda))
            .FirstOrDefaultAsync()
            ;

        if (comanda == null)
        {
            return NotFound();
        }

        var comandaItens = await _dbContext.ComandaItens.AsNoTracking()
                .Where(ci => ci.ComandaId == id)
                .Select(ci => new ComandaItemByIdDto(ci.Id, ci.CardapioItemId, ci.CardapioItem.Titulo))
                .TagWith(nameof(GetComanda) + "Itens")
                .ToListAsync()
                ;

        var respostaDto = new ComandaByIdDto(comanda.numeroMesa, comanda.nomeCliente, comanda.situacaoComanda, comandaItens);
        return Ok(respostaDto);
    }


    [SwaggerOperation(summary: "Adiciona uma comanda ao banco de dados")]
    [SwaggerResponse(201, "Comanda cadastrada com sucesso")]
    [HttpPost]
    public async Task<ActionResult<ComandaCreateResponseDto>> PostComanda(ComandaCreateDto comandaDto)
    {
        var comanda = new Comanda
        {
            NumeroMesa = comandaDto.NumeroMesa,
            NomeCliente = comandaDto.NomeCliente,
            SituacaoComanda = true
        };

        await _dbContext.Comandas.AddAsync(comanda);

        foreach (int item in comandaDto.CardapioItens)
        {
            var comandaItem = new ComandaItem
            {
                CardapioItemId = item,
                Comanda = comanda
            };

            await _dbContext.ComandaItens.AddAsync(comandaItem);

            var cardapioItem = await _dbContext.CardapioItens.FindAsync(item);

            if (cardapioItem != null && cardapioItem.PossuiPreparo)
            {
                PedidoCozinha pedidoCozinha = new()
                {
                    Comanda = comanda,
                    Situacao = 1,
                    PedidoCozinhaItens = new List<PedidoCozinhaItem>()
                    {
                       new PedidoCozinhaItem()
                       {
                           ComandaItem = comandaItem
                       }
                    }
                };

                await _dbContext.PedidosCozinha.AddAsync(pedidoCozinha);
            }
        }


        await _dbContext.SaveChangesAsync();

        var comandaResponse = new ComandaCreateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return CreatedAtAction("GetComanda", new { id = comanda.Id }, comandaResponse);
    }


    [SwaggerOperation(summary: "Edita uma Comanda", description: "Verifica se os dados a editar são iguais e edita uma Comanda no banco de dados")]
    [SwaggerResponse(201, "Comanda editada com sucesso")]
    [SwaggerResponse(404, "Comanda não encontrada")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ComandaUpdateResponseDto>> PutComanda(int id, ComandaUpdateDto updateDto)
    {
        var comanda = await _dbContext.Comandas.AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (comanda == null)
            return NotFound();

        if (!updateDto.nomeCliente.Equals(comanda.NomeCliente))
            comanda.NomeCliente = updateDto.nomeCliente;

        if (updateDto.numeroMesa != comanda.NumeroMesa)
            comanda.NumeroMesa = updateDto.numeroMesa;


        _dbContext.Comandas.Update(comanda);

        foreach (int item in updateDto.cardapioItens)
        {
            var comandaItem = new ComandaItem
            {
                CardapioItemId = item,
                Comanda = comanda
            };

            await _dbContext.ComandaItens.AddAsync(comandaItem);

            var cardapioItem = await _dbContext.CardapioItens
                .Where(ci => ci.Id == item)
                .FirstOrDefaultAsync();

            if (cardapioItem != null && cardapioItem.PossuiPreparo)
            {
                PedidoCozinha pedidoCozinha = new()
                {
                    Comanda = comanda,
                    Situacao = 1,
                    PedidoCozinhaItens = new List<PedidoCozinhaItem>()
                    {
                       new PedidoCozinhaItem()
                       {
                           ComandaItem = comandaItem
                       }
                    }
                };

                await _dbContext.PedidosCozinha.AddAsync(pedidoCozinha);
            }
        }

        await _dbContext.SaveChangesAsync();

        var updateResponse = new ComandaUpdateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return Ok(updateResponse);
    }


    [SwaggerOperation(summary: "Deleta uma comanda", description: "Deleta uma comanda baseada no ID")]
    [SwaggerResponse(204, "Comanda deletada com sucesso")]
    [SwaggerResponse(404, "Comanda não encontrada")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComanda(int id)
    {
        var comanda = await _dbContext.Comandas.AsNoTracking()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (comanda == null)
            return NotFound();

        _dbContext.Comandas.Remove(comanda);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

}
