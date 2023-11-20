using AutoMapper;
using Microsoft.AspNetCore.Http;
using webapi.Application.Tools;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.DTOs.Imagen.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class ImagenService : IImagenService<IFormFile>
    {
        private readonly IImagenRepository _repository;
        private readonly IFileTool _fileTool;
        private readonly IMapper _mapper;

        public ImagenService(IImagenRepository repository, IFileTool fileTool, IMapper mapper)
        {
            _repository = repository;
            _fileTool = fileTool;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ImageAddRequestDto<IFormFile> request)
        {
            var image = _mapper.Map<ImageAddRequestDto<IFormFile>, Imagen>(request);

            image.Ruta = await _fileTool.SaveImageAsync(request.RequestFile);
            var (height, width) = await _fileTool.GetHeightAndWidthAsync(image.Ruta);
            image.Alto = height;
            image.Ancho = width;

            return await HandleImageAdditionAsync(image);
        }

        public Task<int> CountImagesAsync()
        {
            return _repository.CountImagesAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            const string exMessage = "The image could not be deleted, it may have already been deleted or does not exist.";

            var route = (await _repository.FindAsync(entityId) ??
                throw new UnprocessableEntityException(exMessage)).Ruta;

            if (!await _repository.DeleteAsync(entityId))
                throw new UnprocessableEntityException(exMessage);

            _fileTool.DeleteImage(route);
        }

        public async Task<ImagenResponseDto> GetAsync(int entityId)
        {
            var result = await _repository.FindAsync(entityId) ?? throw new NotFoundException("The requested image couldn't be found.");

            return _mapper.Map<Imagen, ImagenResponseDto>(result);
        }

        public async Task<IEnumerable<ImagenResponseDto>> GetImagesAsync(int page, int quantity)
        {
            var images = await _repository.GetImagesAsync(page, quantity);

            return _mapper.Map<IEnumerable<Imagen>, IEnumerable<ImagenResponseDto>>(images);
        }

        public async Task UpdateAsync(ImageUpdRequestDto request)
        {
            var image = _mapper.Map<ImageUpdRequestDto, Imagen>(request);

            if (!await _repository.UpdateAsync(image))
                throw new UnprocessableEntityException("The image could not be updated, it may does not exist.");
        }

        private async Task<int> HandleImageAdditionAsync(Imagen image)
        {
            try
            {
                return await _repository.CreateAsync(image);
            }
            catch (Exception)
            {
                _fileTool.DeleteImage(image.Ruta);
                throw;
            }
        }
    }
}
