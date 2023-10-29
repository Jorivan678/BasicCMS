using FluentValidation;
using webapi.Core.DTOs.Rol.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Rol
{
    internal class RoleUpdRequestValidator : AbstractValidator<RoleUpdRequestDto>
    {
        public RoleUpdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("Role id mustn't be null or zero.");

            RuleFor(x => x.Descripcion).MaximumLength(Miscellaneous.DescriptionMaxLength).WithSeverity(Severity.Error).WithMessage($"Role description can only contain up to {Miscellaneous.DescriptionMaxLength} characters.")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
