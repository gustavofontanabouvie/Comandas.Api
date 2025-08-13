using Comandas.Api.Database;
using Comandas.Api.DTOs.CardapioItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItensController : ControllerBase
    {
        private readonly ComandasDbContext _dbContext;

        public CardapioItensController(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<CardapioItemCreateResponseDto>> PostCardapioItem(CardapioItemCreateDto cardapioItemCreateDto)
        {
            var cardapioItem = new CardapioItem
            {
                Descricao = cardapioItemCreateDto.descricao,
                Titulo = cardapioItemCreateDto.titulo,
                Preco = cardapioItemCreateDto.preco,
                PossuiPreparo = cardapioItemCreateDto.possuiPreparo
            };

            _dbContext.CardapioItens.Add(cardapioItem);
            await _dbContext.SaveChangesAsync();

            var responseDto = new CardapioItemCreateResponseDto(cardapioItem.Id, cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.PossuiPreparo);

            return CreatedAtAction("GetCardapioItem", new { id = cardapioItem.Id }, responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardapioItem>>> GetCardapioItens()
        {
            return await _dbContext.CardapioItens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardapioItemByIdDto>> GetCardapioItem(int id)
        {
            var cardapioItem = await _dbContext.CardapioItens.FindAsync(id);

            if (cardapioItem == null)
                return NotFound();

            var itemByIdDto = new CardapioItemByIdDto(cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.Preco);

            return itemByIdDto;
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto)
        {
            var cardapioItem = await _dbContext.CardapioItens.FindAsync(id);

            if (cardapioItem == null)
                return NotFound();

            cardapioItem.Titulo = updateDto.titulo;
            cardapioItem.Descricao = updateDto.descricao;
            cardapioItem.Preco = updateDto.preco;
            cardapioItem.PossuiPreparo = updateDto.possuiPreparo;

            _dbContext.CardapioItens.Update(cardapioItem);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCardapioItem(int id)
        {
            var cardapioItem = await _dbContext.CardapioItens.FindAsync(id);

            if (cardapioItem == null)
                return NotFound();

            _dbContext.CardapioItens.Remove(cardapioItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
