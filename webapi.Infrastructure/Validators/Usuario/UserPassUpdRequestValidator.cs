using FluentValidation;
using webapi.Core.DTOs.Usuario;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Usuario
{
    public sealed class UserPassUpdRequestValidator : AbstractValidator<UserPassUpdateDto>
    {
        public UserPassUpdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("User id mustn't be null or zero.");

            RuleFor(x => x.OldPassword).NotEmpty().WithSeverity(Severity.Error).WithMessage("The cuurrent password cannot be null or empty.");

            RuleFor(x => x.NewPassword).NotEmpty().WithSeverity(Severity.Error).WithMessage("The new password cannot be null or empty.")
                .MinimumLength(Miscellaneous.PasswordMinLength).WithMessage("The new password must contain at least 8 characters.")
                .Matches(Miscellaneous.PasswordRegex).WithMessage("The new password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
        }
    }
}