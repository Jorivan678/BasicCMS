using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Application.Tools.DTOs;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.DTOs.Comentario.Response;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _service;

        public ComentarioController(IComentarioService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ComentarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("{articleId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ComentarioResponseDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int articleId, [FromQuery]int page, [FromQuery]int size)
        {
            var result = await _service.GetCommentsAsync(page, size, articleId);

            return Ok(result);
        }

        [HttpGet("get-count/{articleId:int}")]
        [ProducesResponseType(typeof(CountResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountAsync(int articleId)
        {
            var result = await _service.CountComentariosAsync(articleId);

            return Ok(new CountResponseDto(result));
        }

        [HttpPost("add-new")]
        [ProducesResponseType(typeof(NewIdResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> PostAsync([FromForm] ComentarioAddRequestDto request)
        {
            var result = await _service.CreateAsync(request);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;

            return Created($"{scheme}://{host}/api/comments/{result}", new NewIdResponseDto(result));
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> PutAsync([FromForm] ComentarioUpdRequestDto request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = $"{Roles.User},{Roles.Admin}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
