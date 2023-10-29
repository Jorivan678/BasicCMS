using AutoMapper;
using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.DTOs.Usuario.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountUsersAsync()
        {
            return _repository.CountUsersAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            if (!await _repository.DeleteAsync(entityId))
                throw new UnprocessableEntityException("The user could not be deleted, it may have already been deleted or does not exist.");
        }

        public async Task<UserResponseDto> GetAsync(int entityId)
        {
            var user = await _repository.FindAsync(entityId) ?? throw new NotFoundException("The user you requested does not exist.");

            return _mapper.Map<Usuario, UserResponseDto>(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersAsync(int page, int quantity)
        {
            var users = await _repository.GetUsersAsync(page, quantity);

            return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UserResponseDto>>(users);
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersAuthorsAsync()
        {
            var users = await _repository.GetUsersAuthorsAsync();

            return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UserResponseDto>>(users);
        }

        public async Task<int> RegisterAsync(UserAddRequestDto request)
        {
            if (await _repository.UserNameExistsAsync(request.NombreUsuario))
                throw new ConflictException("The user name already exists.");

            var user = _mapper.Map<UserAddRequestDto, Usuario>(request);

            return await _repository.CreateAsync(user);
        }

        public async Task UpdateAsync(UserUpdRequestDto request)
        {
            if (await _repository.UserNameExistsAsync(request.NombreUsuario, request.Id))
                throw new ConflictException("The user name already exists.");

            var user = _mapper.Map<UserUpdRequestDto, Usuario>(request);

            if (!await _repository.UpdateAsync(user))
                throw new UnprocessableEntityException("The user could not be updated, it may does not exist.");
        }
    }
}
