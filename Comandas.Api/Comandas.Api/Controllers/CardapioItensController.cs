using Comandas.Api.Database;
using Comandas.Api.DTOs.CardapioItem;
using Comandas.Application.Interfaces;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;


namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItensController : ControllerBase
    {
        private readonly ICardapioItemService _cardapioItemService;
        private readonly ILogger<CardapioItensController> _logger;

        public CardapioItensController(ICardapioItemService cardapioItemService, ILogger<CardapioItensController> logger)
        {
            _cardapioItemService = cardapioItemService;
            _logger = logger;
        }


        [SwaggerOperation(summary: "Criação de um novo CardapioItem")]
        [SwaggerResponse(201, "Caso o item seja criado com sucesso")]
        [HttpPost]
        public async Task<ActionResult<CardapioItemCreateResponseDto>> PostCardapioItem([FromBody] CardapioItemCreateDto cardapioItemCreateDto, CancellationToken cancellationToken)
        {
            var retorno = await _cardapioItemService.PostCardapioItem(cardapioItemCreateDto, cancellationToken);

            return CreatedAtAction("GetCardapioItem", new { id = retorno.id }, retorno);
        }


        [SwaggerOperation(summary: "Retorno de uma lista com todos os cardapioItens cadastrados")]
        [SwaggerResponse(200, "Retorna a lista dos CardapioItens")]
        [SwaggerResponse(204, "Não existem itens cadastrados")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardapioItem>>> GetCardapioItens()
        {
            var cardapioItens = await _cardapioItemService.GetCardapioItens();

            if (cardapioItens.IsNullOrEmpty())
                return NoContent();

            return Ok(cardapioItens);
        }


        [SwaggerOperation(summary: "Retorna um cardapioItem", description: "Retorna um cardápioItem baseado em um ID")]
        [SwaggerResponse(404, "CardapioItem não encontrado")]
        [SwaggerResponse(200, "CardapioItem encontrado com sucesso")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CardapioItemByIdDto>> GetCardapioItem(int id, CancellationToken cancellationToken)
        {
            var cardapioItem = await _cardapioItemService.GetCardapioItem(id, cancellationToken);

            if (cardapioItem == null)
                return NotFound();

            return Ok(cardapioItem);
        }

        [SwaggerOperation(summary: "Edita um CardapioItem", description: "Verifica se os campos a editar são iguais e edita um CardapioItem pelo ID")]
        [SwaggerResponse(404, "Item não encontrado")]
        [SwaggerResponse(200, "Item editado com sucesso")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CardapioItemUpdateResponseDto>> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto, CancellationToken cancellationToken)
        {
            var cardapioItem = await _cardapioItemService.UpdateCardapioItem(id, updateDto, cancellationToken);

            if (cardapioItem == null)
                return NotFound();

            var cardapioItemResponse = new CardapioItemUpdateResponseDto(cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.Preco, cardapioItem.PossuiPreparo);

            return Ok(cardapioItemResponse);
        }

        [SwaggerOperation(summary: "Exclui um item do cardápio", Description = "Exclui um item do cardápio baseado em um ID")]
        [SwaggerResponse(204, "Sem conteúdo quando DELETE ocorrer com sucesso")]
        [SwaggerResponse(404, "Não encontrado quando recurso não existir")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCardapioItem(int id, CancellationToken cancellationToken)
        {
            var cardapioItem = await _cardapioItemService.DeleteCardapioItem(id, cancellationToken);

            if (cardapioItem == null)
                return NotFound();

            return NoContent();
        }
    }
}
