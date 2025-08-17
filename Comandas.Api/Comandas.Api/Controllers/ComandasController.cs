using Comandas.Api.Database;
using Comandas.Api.DTOs.Comanda;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


    /// <summary>
    /// Retorna a lista de todas as comandas
    /// </summary>
    /// <returns>ActionResult</returns>
    /// <response code="200">Retorna a lista de comandas</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetComandas()
    {
        return await _dbContext.Comandas.ToListAsync();
    }

    /// <summary>
    /// Busca uma comanda pelo seu ID no banco de dados
    /// </summary>
    /// <param name="id">O ID da comanda que deseja buscar</param>
    /// <returns>ActionResult</returns>
    /// <response code="200">Comanda encontrada com sucesso</response>
    /// <response code="404">Id não encontrado</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ComandaByIdDto>> GetComanda(int id)
    {
        var comanda = await _dbContext.Comandas.FindAsync(id);

        if (comanda == null)
        {
            return NotFound();
        }

        var respostaDto = new ComandaByIdDto(comanda.NumeroMesa, comanda.NomeCliente, comanda.SituacaoComanda);
        return Ok(respostaDto);
    }

    /// <summary>
    /// Adiciona uma comanda ao banco de dados
    /// </summary>
    /// <param name="comandaDto">Objeto com os campos necessários para criar uma comanda</param>
    /// <returns>ActionResult</returns>
    /// <response code="201">Comanda cadastrada com sucesso</response>
    [HttpPost]
    public async Task<ActionResult<ComandaCreateResponseDto>> PostComanda(ComandaCreateDto comandaDto)
    {
        var comanda = new Comanda
        {
            NumeroMesa = comandaDto.NumeroMesa,
            NomeCliente = comandaDto.NomeCliente,
            SituacaoComanda = true
        };

        _dbContext.Comandas.Add(comanda);

        foreach (int item in comandaDto.CardapioItens)
        {
            var comandaItem = new ComandaItem
            {
                CardapioItemId = item,
                Comanda = comanda
            };

            _dbContext.ComandaItens.Add(comandaItem);

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


    /// <summary>
    /// Edita uma comanda pelo Id
    /// </summary>
    /// <param name="id">ID da comanda resgatado no banco de dados</param>
    /// <param name="updateDto">Objeto com os campos editados</param>
    /// <returns>ActionResult</returns>
    /// <response code="200">Edição feita com sucesso</response>
    /// <response code="404">ID não encontrado</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<ComandaUpdateResponseDto>> PutComanda(int id, ComandaUpdateDto updateDto)
    {
        var comanda = await _dbContext.Comandas.FindAsync(id);

        if (comanda == null)
            return NotFound();


        comanda.NomeCliente = updateDto.nomeCliente;
        comanda.NumeroMesa = updateDto.numeroMesa;


        _dbContext.Comandas.Update(comanda);

        foreach (int item in updateDto.cardapioItens)
        {
            var comandaItem = new ComandaItem
            {
                CardapioItemId = item,
                Comanda = comanda
            };

            _dbContext.ComandaItens.Add(comandaItem);

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

        var updateResponse = new ComandaUpdateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return Ok(updateResponse);
    }

    /// <summary>
    /// Deleta uma comanda pelo ID
    /// </summary>
    /// <param name="id">ID da comanda a ser deletada</param>
    /// <returns>ActionResult</returns>
    /// <response code="200">Comanda deletada com sucesso</response>
    /// <response code="404">ID não encontrada</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComanda(int id)
    {
        var comanda = await _dbContext.Comandas.FindAsync(id);

        if (comanda == null)
            return NotFound();

        _dbContext.Comandas.Remove(comanda);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

}
