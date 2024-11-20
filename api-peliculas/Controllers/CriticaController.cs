using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration.UserSecrets;

[ApiController]
//[Authorize]
[Route("api/criticas")]
public class CriticaController : ControllerBase
{
    private readonly ICriticaService _criticaService;
    private readonly IPeliculaService _peliculaService;
    //private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public CriticaController(ICriticaService criticaService, IPeliculaService peliculaService, IConfiguration configuration) //UserManager<ApplicationUser> userManager,
    {
        _criticaService = criticaService;
        _peliculaService = peliculaService;
        //_userManager = userManager;
        _configuration = configuration;
    }

    [HttpGet("Listado")]
    public ActionResult<List<Critica>> GetAll()
    {
        try
        {
            return Ok(_criticaService.GetAll());
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Critica> GetById(int id)
    {
        Critica? critica = _criticaService.GetById(id);
        if ( critica is null ) return NotFound();
        return Ok(critica);
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> NuevaCritica(CriticaDTO c)
    {
        //var userName = User.FindFirstValue(ClaimTypes.Name);
        //var user = await _userManager.FindByNameAsync(userName);
        //Console.WriteLine(user.Id);
        //return NotFound();
         Critica critica = _criticaService.Create(c);//, user.Id);
         return CreatedAtAction(nameof(GetById), new { id = critica.Id}, critica);
    }
    
    
    [HttpDelete("Eliminar/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        // Obtener el ID del usuario autenticado
        //var userName = User.FindFirstValue(ClaimTypes.Name);
        //var user = await _userManager.FindByNameAsync(userName);

        try
        {
            // Llamar al servicio para eliminar la crítica
            var deleted = _criticaService.Delete(id);//, user.Id);

            if (deleted)
            {
                return NoContent(); // Eliminación exitosa
            }

            return Forbid("No tienes permiso para eliminar esta crítica.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Manejar otros posibles errores
        }
    }
    
    [HttpPut("Actualizar/{id}")]
    public async Task<ActionResult> Update(int id, CriticaDTO c)
    {
        // Obtener el ID del usuario autenticado
        //var userName = User.FindFirstValue(ClaimTypes.Name);
        //var user = await _userManager.FindByNameAsync(userName);
        //Console.WriteLine(user.Id);

        try
        {
            // Llamar al servicio para actualizar la crítica
            Critica critica = _criticaService.Update(id, c);//, user.Id);
            if ( critica is null ) return NotFound(new {Message = $"No se pudo actualizar la critica con id: {id}"});
            return CreatedAtAction(nameof(GetById), new { id = critica.Id}, critica);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid("No tienes permiso para modificar esta crítica.");
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem(detail: e.Message, statusCode: 500);
        }
    }

    // // Obtener las críticas de un usuario específico por su userId
    // [HttpGet("byuser/{UsuarioId}")]
    // public ActionResult<IEnumerable<Critica>> GetByUser(string UsuarioId)
    // {
    //     // Llamar al servicio para obtener las críticas por el userId proporcionado
    //     var criticas = _criticaService.GetByUser(UsuarioId);

    //     if (criticas == null || !criticas.Any())
    //     {
    //         return NotFound($"No se encontraron críticas para el usuario con Id: {UsuarioId}");
    //     }

    //     return Ok(criticas);
    // }
}