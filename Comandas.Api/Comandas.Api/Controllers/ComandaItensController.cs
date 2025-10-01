using Comandas.Api.Database;
using Comandas.Api.DTOs.ComandaItem;
using Comandas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaItensController : ControllerBase
    {
        private readonly IComandaItemService _comandaItemService;
        private readonly ILogger _logger;

        public ComandaItensController(ILogger<ComandaItensController> logger, IComandaItemService comandaItemService)
        {
            _logger = logger;
            _comandaItemService = comandaItemService;
        }


        [SwaggerOperation(summary: "Retorna um ComandaItem", description: "Retorna o ID da Comanda, também acessa a tabela CardapioItem e retorna o ID")]
        [SwaggerResponse(404, "ComandaItem não encotrado")]
        [SwaggerResponse(200, "ComandaItem encontrado com sucesso")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaItemResponseDto>> GetComandaItem(int id)
        {
            var comandaItem = await _comandaItemService.GetComandaItemById(id);

            if (comandaItem == null)
                return NotFound();

            return Ok(comandaItem);
        }
    }
}
