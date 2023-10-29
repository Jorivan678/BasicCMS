using FluentValidation;
using webapi.Core.DTOs.Usuario.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Usuario
{
    internal class UserAddRequestValidator : AbstractValidator<UserAddRequestDto>
    {
        public UserAddRequestValidator() 
        {
            RuleFor(x => x.NombreUsuario).NotEmpty().WithSeverity(Severity.Error).WithMessage("User name mustn't be null or empty.")
                .Matches(Miscellaneous.UserNameRegex).WithSeverity(Severity.Warning).WithMessage("The user name must consist of letters, numbers, or underscores.")
                .MaximumLength(Miscellaneous.UserNameMaxLength).WithSeverity(Severity.Error).WithMessage($"User name can only contain up to {Miscellaneous.UserNameMaxLength} characters.");

            RuleFor(x => x.Nombre).NotEmpty().WithSeverity(Severity.Error).WithMessage("Name mustn't be null or empty.")
                .MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"Name can only contain up to {Miscellaneous.RegularMaxLength} characters.");

            RuleFor(x => x.ApellidoP).NotEmpty().WithSeverity(Severity.Error).WithMessage("First last name mustn't be null or empty.")
                .MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"First last name can only contain up to {Miscellaneous.RegularMaxLength} characters.");

            RuleFor(x => x.ApellidoM).MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"Second last name can only contain up to {Miscellaneous.RegularMaxLength} characters.");

            RuleFor(x => x.Password).NotEmpty().WithSeverity(Severity.Error).WithMessage("Password cannot be null or empty.")
                .MinimumLength(Miscellaneous.PasswordMinLength).WithMessage($"Password must contain at least {Miscellaneous.PasswordMinLength} characters.")
                .Matches(Miscellaneous.PasswordRegex).WithMessage("The password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
        }
    }
}
