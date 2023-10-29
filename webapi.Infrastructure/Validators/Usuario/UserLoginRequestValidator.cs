using FluentValidation;
using webapi.Core.DTOs.Usuario;

namespace webapi.Infrastructure.Validators.Usuario
{
    internal class UserLoginRequestValidator : AbstractValidator<UserLoginRequestDto>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithSeverity(Severity.Error).WithMessage("User name mustn't be null or empty.");

            RuleFor(x => x.Password).NotEmpty().WithSeverity(Severity.Error).WithMessage("Password mustn't be null or empty.");
        }
    }
}
