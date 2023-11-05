using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Application.Tools;
using webapi.Application.Tools.DTOs;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Articulo.Request;
using webapi.Core.DTOs.Articulo.Response;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/articles")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _service;

        public ArticuloController(IArticuloService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ArticuloResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ArticuloResponseDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromQuery]int page, [FromQuery]int size, [FromQuery]string categories = "", [FromQuery]int authorId = 0)
        {
            var result = await _service.GetArticlesAsync(page, size, categories.ToCategoryArray(), authorId);

            return Ok(result);
        }

        [HttpGet("get-count")]
        [ProducesResponseType(typeof(CountResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountAsync([FromQuery] string categories = "", [FromQuery] int authorId = 0)
        {
            var result = await _service.CountArticlesAsync(categories.ToCategoryArray(), authorId);

            return Ok(new CountResponseDto(result));
        }

        [HttpPost("add-new")]
        [ProducesResponseType(typeof(NewIdResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> PostAsync([FromForm] ArticuloAddRequestDto request)
        {
            var result = await _service.CreateAsync(request);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;

            return Created($"{scheme}://{host}/api/articles/{result}", new NewIdResponseDto(result));
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> PutAsync([FromForm] ArticuloUpdRequestDto request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
