using webapi.Core.DTOs;
using webapi.Core.DTOs.Usuario;
using webapi.Core.Exceptions;
using webapi.Core.Interfaces.Repositories;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUsuarioRepository _userRepository;
        private readonly ITokenService _tokenService;
        
        public AuthService(IUsuarioRepository userRepository, IAuthRepository authRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _authRepository = authRepository;
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            if (!await _authRepository.AssignRoleAsync(userId, roleId))
                throw new UnprocessableEntityException("The role could not be assigned. The user may not exist.");
        }

        public async Task<TokenResponseDto> LogInAsync(UserLoginRequestDto request)
        {
            var user = await _userRepository.FindAsync(request.UserName) ?? throw new ForbiddenException("The user doesn't exist.");

            if (!await _authRepository.CheckPasswordAsync(user.Id, request.Password))
                throw new ForbiddenException("The password is not correct.");

            var roles = await _authRepository.GetUserRolesAsync(user.Id);

            return new(_tokenService.CreateToken(user, roles));
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            if (!await _authRepository.RemoveRoleAsync(userId, roleId))
                throw new UnprocessableEntityException("The role could not be remove from user. The user may not exist.");
        }

        public async Task UpdatePasswordAsync(UserPassUpdateDto request)
        {
            if (!await _authRepository.CheckPasswordAsync(request.Id, request.OldPassword))
                throw new ForbiddenException("The attempted password didn't match with current password.");

            if (!await _authRepository.UpdatePasswordAsync(request.Id, request.NewPassword))
                throw new UnprocessableEntityException("The user password. The user may not exist.");
        }
    }
}
