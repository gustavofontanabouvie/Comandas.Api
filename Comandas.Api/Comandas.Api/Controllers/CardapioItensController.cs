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

        /// <summary>
        /// Criação de um novo CardapioItem
        /// </summary>
        /// <param name="cardapioItemCreateDto">Itens necessarios para criar um CardapioItem</param>
        /// <returns>ActionResult</returns>
        /// <response code="201">Caso o item seja criado com sucesso</response>
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

        /// <summary>
        /// Retorno de uma lista com todos os cardapioItens cadastrados
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a lista dos cardapioItens</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardapioItem>>> GetCardapioItens()
        {
            return await _dbContext.CardapioItens.ToListAsync();
        }

        /// <summary>
        /// Retorna um cardapioItem pelo seu ID
        /// </summary>
        /// <param name="id">Id do cardapioItem a ser buscado</param>
        /// <returns>ActionResult</returns>
        /// <response code="404">CardapioItem não encontrado</response>
        /// <response code="200">CardapioItem encontrado com sucesso</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CardapioItemByIdDto>> GetCardapioItem(int id)
        {
            var cardapioItem = await _dbContext.CardapioItens.FindAsync(id);

            if (cardapioItem == null)
                return NotFound();

            var itemByIdDto = new CardapioItemByIdDto(cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.Preco);

            return Ok(itemByIdDto);
        }

        /// <summary>
        /// Edita um CardapioItem pelo seu ID
        /// </summary>
        /// <param name="id">Id que sera buscado no banco</param>
        /// <param name="updateDto">Parametros necessários para editar um cardapioItem</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">CardapioItem editado com sucesso</response>
        /// <response code="404">ID não encontrado</response>
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

        /// <summary>
        /// Deleta um CardapioItem pelo seu ID
        /// </summary>
        /// <param name="id">Id do cardapioItem a ser deletado</param>
        /// <returns>ActionResult</returns>
        /// <response code="404">Id não encontrado</response>
        /// <response code"200">CardapioItem deletado com sucesso</response>
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
