using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Application.Tools.DTOs;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.DTOs.Usuario.Response;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("get-authors")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var result = await _service.GetUsersAuthorsAsync();

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAsync([FromQuery]int page, [FromQuery]int size)
        {
            var result = await _service.GetUsersAsync(page, size);

            return Ok(result);
        }

        [HttpGet("get-count")]
        [ProducesResponseType(typeof(CountResponseDto), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountAsync()
        {
            var result = await _service.CountUsersAsync();

            return Ok(new CountResponseDto(result));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(NewIdResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status409Conflict)]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromForm]UserAddRequestDto request)
        {
            var result = await _service.RegisterAsync(request);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;

            return Created($"{scheme}://{host}/api/users/{result}", new NewIdResponseDto(result));
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> PutAsync([FromForm]UserUpdRequestDto request)
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
