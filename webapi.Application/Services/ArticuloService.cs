using AutoMapper;
using webapi.Core.DTOs.Articulo.Request;
using webapi.Core.DTOs.Articulo.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repository;
        private readonly IMapper _mapper;

        public ArticuloService(IArticuloRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountArticlesAsync(string[] categories, int authorId)
        {
            if (categories.Length > 0 && authorId > 0)
                return _repository.CountArticulosAsync(categories, authorId);
            if (categories.Length > 0)
                return _repository.CountArticulosAsync(categories);
            if (authorId > 0)
                return _repository.CountArticulosAsync(authorId);

            return _repository.CountArticulosAsync();
        }

        public Task<int> CreateAsync(ArticuloAddRequestDto request)
        {
            var art = _mapper.Map<ArticuloAddRequestDto, Articulo>(request);

            return _repository.CreateAsync(art);
        }

        public async Task DeleteAsync(int entityId)
        {
            if (!await _repository.DeleteAsync(entityId))
                throw new UnprocessableEntityException("The article could not be deleted, it may have already been deleted or does not exist.");
        }

        public async Task<IEnumerable<ArticuloResponseDto>> GetArticlesAsync(int page, int quantity, string[] categories, int authorId)
        {
            IEnumerable<Articulo> articles;

            if (categories.Length > 0 && authorId > 0)
                articles = await _repository.GetArticulosAsync(page, quantity, categories, authorId);
            else if (categories.Length > 0)
                articles = await _repository.GetArticulosAsync(page, quantity, categories);
            else if (authorId > 0)
                articles = await _repository.GetArticulosAsync(page, quantity, authorId);
            else
                articles = await _repository.GetArticulosAsync(page, quantity);

            return _mapper.Map<IEnumerable<Articulo>, IEnumerable<ArticuloResponseDto>>(articles);
        }

        public async Task<ArticuloResponseDto> GetAsync(int entityId)
        {
            var article = await _repository.FindAsync(entityId) ?? throw new NotFoundException("The article you requested does not exist.");

            return _mapper.Map<Articulo, ArticuloResponseDto>(article);
        }

        public async Task UpdateAsync(ArticuloUpdRequestDto request)
        {
            var article = _mapper.Map<ArticuloUpdRequestDto, Articulo>(request);

            if (!await _repository.UpdateAsync(article))
                throw new UnprocessableEntityException("The article could not be updated, it may does not exist.");
        }
    }
}
