using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SADPERAsistencia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        [HttpGet(Name = "GetValue")]
        [Authorize]
        public IActionResult GetValue()
        {
            return Ok("Hola acceso verificado");
        }
    }
}
