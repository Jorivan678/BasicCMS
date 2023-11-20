using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using webapi.Core.Interfaces.Services;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Rol.Request;
using webapi.Core.DTOs.Rol.Response;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public sealed class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<RoleResponseDto>), StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _service.GetRolesAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RoleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetRoleAsync(id);

            return Ok(result);
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> PutAsync([FromForm] RoleUpdRequestDto request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }
    }
}