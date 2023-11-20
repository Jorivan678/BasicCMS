using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using webapi.Application.Tools.DTOs;
using webapi.Core.DTOs;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.DTOs.Imagen.Response;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/images")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status500InternalServerError)]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenService<IFormFile> _service;

        public ImagenController(IImagenService<IFormFile> service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ImagenResponseDto>), StatusCodes.Status200OK)]
        [Authorize(Roles = $"{Roles.Editor},{Roles.Admin}")]
        public async Task<IActionResult> GetAsync([FromQuery]int page, [FromQuery]int size)
        {
            var result = await _service.GetImagesAsync(page, size);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ImagenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{Roles.Editor},{Roles.Admin}")]
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
            var result = await _service.CountImagesAsync();

            return Ok(new CountResponseDto(result));
        }

        [HttpPost("add-new")]
        [ProducesResponseType(typeof(NewIdResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = $"{Roles.Editor},{Roles.Admin}")]
        public async Task<IActionResult> PostAsync([FromForm] ImageAddRequestDto<IFormFile> request)
        {
            var result = await _service.AddAsync(request);

            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;

            return Created($"{scheme}://{host}/api/images/{result}", new NewIdResponseDto(result));
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorObject), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = $"{Roles.Editor},{Roles.Admin}")]
        public async Task<IActionResult> PutAsync([FromForm] ImageUpdRequestDto request)
        {
            await _service.UpdateAsync(request);

            return NoContent();
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseObject), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = $"{Roles.Editor},{Roles.Admin}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
