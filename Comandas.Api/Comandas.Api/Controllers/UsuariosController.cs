using Comandas.Api.Database;
using Microsoft.AspNetCore.Mvc;


namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ComandasDbContext _dbContext;

    public UsuariosController(ComandasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //[HttpGet]
    //public async Task<ActionResult<Usuarios>>
}
