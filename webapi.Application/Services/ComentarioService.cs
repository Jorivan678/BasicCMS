using AutoMapper;
using webapi.Application.Tools;
using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.DTOs.Comentario.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;
using webapi.Core.StaticData;

namespace webapi.Application.Services
{
    internal class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _repository;
        private readonly IAuthValidator _authValidator;
        private readonly IMapper _mapper;

        public ComentarioService(IComentarioRepository repository, IAuthValidator authValidator, IMapper mapper)
        {
            _repository = repository;
            _authValidator = authValidator;
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
            const string ueExMessage = "The comment could not be deleted, it may have already been deleted or does not exist.";

            var authorId = (await _repository.FindAsync(entityId) 
                ?? throw new UnprocessableEntityException(ueExMessage)).AutorId;

            if (!_authValidator.HasId(authorId) && !_authValidator.HasRole(Roles.Admin))
                throw new ForbiddenException("You don't have permission to perform this action.");

            if (!await _repository.DeleteAsync(entityId))
                throw new UnprocessableEntityException(ueExMessage);
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
            if (!_authValidator.HasId(request.AutorId))
                throw new ForbiddenException("You don't have permission to perform this action.");

            var comment = _mapper.Map<ComentarioUpdRequestDto, Comentario>(request);

            if (!await _repository.UpdateAsync(comment))
                throw new UnprocessableEntityException("The comment could not be updated, it may does not exist.");
        }
    }
}
