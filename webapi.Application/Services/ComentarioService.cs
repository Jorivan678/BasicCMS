using AutoMapper;
using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.DTOs.Comentario.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _repository;
        private readonly IMapper _mapper;

        public ComentarioService(IComentarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountComentariosAsync(int articleId)
        {
            return _repository.CountComentariosAsync(articleId);
        }

        public Task<int> CreateAsync(ComentarioAddRequestDto request)
        {
            var comment = _mapper.Map<ComentarioAddRequestDto, Comentario>(request);

            return _repository.CreateAsync(comment);
        }

        public async Task DeleteAsync(int entityId)
        {
            if (!await _repository.DeleteAsync(entityId))
                throw new UnprocessableEntityException("The comment could not be deleted, it may have already been deleted or does not exist.");
        }

        public async Task<ComentarioResponseDto> GetAsync(int entityId)
        {
            var comment = await _repository.FindAsync(entityId) ?? throw new NotFoundException("The comment you requested does not exist.");

            return _mapper.Map<Comentario, ComentarioResponseDto>(comment);
        }

        public async Task<IEnumerable<ComentarioResponseDto>> GetCommentsAsync(int page, int quantity, int articleId)
        {
            var commments = await _repository.GetComentariosAsync(page, quantity, articleId);

            return _mapper.Map<IEnumerable<Comentario>, IEnumerable<ComentarioResponseDto>>(commments);
        }

        public async Task UpdateAsync(ComentarioUpdRequestDto request)
        {
            var comment = _mapper.Map<ComentarioUpdRequestDto, Comentario>(request);

            if (!await _repository.UpdateAsync(comment))
                throw new UnprocessableEntityException("The comment could not be updated, it may does not exist.");
        }
    }
}
