using AutoMapper;
using webapi.Core.DTOs.Rol.Request;
using webapi.Core.DTOs.Rol.Response;
using webapi.Core.Entities;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RoleResponseDto> GetRoleAsync(int roleId)
        {
            var role = await _repository.FindAsync(roleId) ?? throw new NotFoundException("The role you requested does not exist.");

            return _mapper.Map<Rol, RoleResponseDto>(role);
        }

        public async Task<IEnumerable<RoleResponseDto>> GetRolesAsync()
        {
            var roles = await _repository.GetRolesAsync();

            return _mapper.Map<IEnumerable<Rol>, IEnumerable<RoleResponseDto>>(roles);
        }

        public async Task UpdateAsync(RoleUpdRequestDto request)
        {
            var role = _mapper.Map<RoleUpdRequestDto, Rol>(request);

            if (!await _repository.UpdateAsync(role))
                throw new UnprocessableEntityException("The role could not be updated, it may does not exist.");
        }
    }
}
