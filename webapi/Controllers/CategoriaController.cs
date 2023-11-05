using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Application.Tools.DTOs;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<CategoriaResponseDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromQuery]int page, [FromQuery]int size = 10)
        {
            var result = await _service.GetCategoriesAsync(page, size);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoriaResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("get-count")]
        [ProducesResponseType(typeof(CountResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountAsync()
        {
            var result = await _service.CountCategoriesAsync();

            return Ok(new CountResponseDto(result));
        }

        [HttpPost("add-new")]
        [ProducesResponseType(typeof(NewIdResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status409Conflict)]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> PostAsync([FromForm] CategoriaAddRequestDto request)
        {
            var result = await _service.CreateAsync(request);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;

            return Created($"{scheme}://{host}/api/categories/{result}", new NewIdResponseDto(result));
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Editor)]
        public async Task<IActionResult> PutAsync([FromForm] CategoriaUpdRequestDto request)
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
