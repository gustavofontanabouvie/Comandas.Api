using Comandas.Api.Database;
using Comandas.Api.DTOs.ComandaItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaItensController : ControllerBase
    {
        private readonly ComandasDbContext _dbContext;

        public ComandaItensController(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [SwaggerOperation(summary: "Retorna um ComandaItem", description: "Retorna um ComandaItem baseado em seu ID, também acessa a tabela CardapioItem e retorna o ID")]
        [SwaggerResponse(404, "ComandaItem não encotrado")]
        [SwaggerResponse(200, "ComandaItem encontrado com sucesso")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaItemResponseDto>> GetComandaItem(int id)
        {
            var comanItem = await (from comandaItem in _dbContext.ComandaItens
                                   join comanda in _dbContext.Comandas
                                     on comandaItem.ComandaId equals comanda.Id
                                   join cardapioItem in _dbContext.CardapioItens
                                     on comandaItem.CardapioItemId equals cardapioItem.Id
                                   where comandaItem.Id == id
                                   select new
                                   {
                                       ComandaId = comanda.Id,
                                       CardapioItemId = cardapioItem.Id
                                   }).FirstOrDefaultAsync();

            if (comanItem == null)
                return NotFound();

            var responseComandaItem = new ComandaItemResponseDto(comanItem.ComandaId, comanItem.CardapioItemId);

            return Ok(responseComandaItem);
        }
    }
}
