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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetComandas()
    {
        return await _dbContext.Comandas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComandaByIdDto>> GetComanda(int id)
    {
        var comanda = await _dbContext.Comandas.FindAsync(id);

        if (comanda == null)
        {
            return NotFound();
        }

        var respostaDto = new ComandaByIdDto(comanda.NumeroMesa, comanda.NomeCliente, comanda.SituacaoComanda);
        return respostaDto;
    }

    [HttpPost]
    public async Task<ActionResult<ComandaCreateResponseDto>> PostComanda(ComandaCreateDto comandaDto)
    {
        var comanda = new Comanda
        {
            NumeroMesa = comandaDto.NumeroMesa,
            NomeCliente = comandaDto.NomeCliente,

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
        }

        await _dbContext.SaveChangesAsync();

        var comandaResponse = new ComandaCreateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return CreatedAtAction("GetComanda", new { id = comanda.Id }, comandaResponse);
    }

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
        }

        await _dbContext.SaveChangesAsync();

        var updateResponse = new ComandaUpdateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return Ok(updateResponse);
    }

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
