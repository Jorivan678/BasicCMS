using AutoMapper;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.DTOs.Categoria.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountCategoriesAsync()
        {
            return _repository.CountCategoriesAsync();
        }

        public async Task<int> CreateAsync(CategoriaAddRequestDto request)
        {
            if (await _repository.CategoryExistsAsync(request.Nombre))
                throw new ConflictException($"The category {request.Nombre} already exists.");

            var category = _mapper.Map<CategoriaAddRequestDto, Categoria>(request);

            return await _repository.CreateAsync(category);
        }

        public async Task DeleteAsync(int entityId)
        {
            if (!await _repository.DeleteAsync(entityId)) 
                throw new UnprocessableEntityException("The category could not be deleted, it may have already been deleted or does not exist.");
        }

        public async Task<CategoriaResponseDto> GetAsync(int entityId)
        {
            var category = await _repository.FindAsync(entityId) ?? throw new NotFoundException("The category you requested does not exist.");

            return _mapper.Map<Categoria, CategoriaResponseDto>(category);
        }

        public async Task<IEnumerable<CategoriaResponseDto>> GetCategoriesAsync(int page, int quantity)
        {
            var categories = await _repository.GetCategoriasAsync(page, quantity);

            return _mapper.Map<IEnumerable<Categoria>, IEnumerable<CategoriaResponseDto>>(categories);
        }

        public async Task UpdateAsync(CategoriaUpdRequestDto request)
        {
            var category = _mapper.Map<CategoriaUpdRequestDto, Categoria>(request);

            if (!await _repository.UpdateAsync(category))
                throw new UnprocessableEntityException("The category could not be updated, it may does not exist.");
        }
    }
}
