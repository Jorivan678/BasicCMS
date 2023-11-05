using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Usuario;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginRequestDto request)
        {
            var token = await _service.LogInAsync(request);

            return Ok(token);
        }

        [HttpPut("roles/assign/{userId:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AssignRoleAsync(int userId, [FromQuery] int roleId)
        {
            await _service.AssignRoleAsync(userId, roleId);

            return NoContent();
        }

        [HttpPut("roles/remove/{userId:int}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> RemoveRoleAsync(int userId, [FromQuery] int roleId)
        {
            await _service.RemoveRoleAsync(userId, roleId);

            return NoContent();
        }

        [HttpPut("user/change-password")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize]
        public async Task<IActionResult> UpdateUserPassAsync([FromForm]UserPassUpdateDto request)
        {
            await _service.UpdatePasswordAsync(request);

            return NoContent();
        }
    }
}
