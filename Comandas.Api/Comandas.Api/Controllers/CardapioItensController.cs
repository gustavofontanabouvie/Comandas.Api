using Comandas.Api.Database;
using Comandas.Api.DTOs.CardapioItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;


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


        [SwaggerOperation(summary: "Criação de um novo CardapioItem")]
        [SwaggerResponse(201, "Caso o item seja criado com sucesso")]
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


        [SwaggerOperation(summary: "Retorno de uma lista com todos os cardapioItens cadastrados")]
        [SwaggerResponse(200, "Retorna a lista dos CardapioItens")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardapioItem>>> GetCardapioItens()
        {
            return await _dbContext.CardapioItens.ToListAsync();
        }


        [SwaggerOperation(summary: "Retorna um cardapioItem", description: "Retorna um cardápioItem baseado em um ID")]
        [SwaggerResponse(404, "CardapioItem não encontrado")]
        [SwaggerResponse(200, "CardapioItem encontrado com sucesso")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CardapioItemByIdDto>> GetCardapioItem(int id)
        {
            var cardapioItem = await _dbContext.CardapioItens.AsNoTracking()
                .Where(ci => ci.Id == id)
                .Select(ci => new CardapioItemByIdDto(ci.Titulo, ci.Descricao, ci.Preco))
                .TagWith(nameof(GetCardapioItem))
                .FirstOrDefaultAsync();

            if (cardapioItem == null)
                return NotFound();

            return Ok(cardapioItem);
        }

        [SwaggerOperation(summary: "Edita um CardapioItem", description: "Verifica se os campos a editar são iguais e edita um CardapioItem pelo ID")]
        [SwaggerResponse(404, "Item não encontrado")]
        [SwaggerResponse(200, "Item editado com sucesso")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CardapioItemUpdateResponseDto>> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto)
        {
            var cardapioItem = await _dbContext.CardapioItens.AsNoTracking()
                .Where(ci => ci.Id == id)
                .FirstOrDefaultAsync();


            if (cardapioItem == null)
                return NotFound();

            if (!cardapioItem.Titulo.Equals(updateDto.titulo))
                cardapioItem.Titulo = updateDto.titulo;

            if (!cardapioItem.Descricao.Equals(updateDto.descricao))
                cardapioItem.Descricao = updateDto.descricao;

            if (cardapioItem.Preco != updateDto.preco)
                cardapioItem.Preco = updateDto.preco;

            if (cardapioItem.PossuiPreparo != updateDto.possuiPreparo)
                cardapioItem.PossuiPreparo = updateDto.possuiPreparo;

            _dbContext.CardapioItens.Update(cardapioItem);

            await _dbContext.SaveChangesAsync();
            var cardapioItemResponse = new CardapioItemUpdateResponseDto(cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.Preco, cardapioItem.PossuiPreparo);

            return Ok(cardapioItemResponse);
        }

        [SwaggerOperation(summary: "Exclui um item do cardápio", Description = "Exclui um item do cardápio baseado em um ID")]
        [SwaggerResponse(204, "Sem conteúdo quando DELETE ocorrer com sucesso")]
        [SwaggerResponse(404, "Não encontrado quando recurso não existir")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCardapioItem(int id)
        {
            var cardapioItem = await _dbContext.CardapioItens
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Id == id);

            if (cardapioItem == null)
                return NotFound();

            _dbContext.CardapioItens.Remove(cardapioItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
