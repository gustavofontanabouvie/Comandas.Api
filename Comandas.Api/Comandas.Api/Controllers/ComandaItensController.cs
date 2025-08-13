using Comandas.Api.Database;
using Comandas.Api.DTOs.ComandaItem;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            return Ok(comanItem);
        }
    }
}
