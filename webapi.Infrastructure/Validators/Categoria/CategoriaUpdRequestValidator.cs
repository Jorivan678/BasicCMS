using FluentValidation;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Categoria
{
    public sealed class CategoriaUpdRequestValidator : AbstractValidator<CategoriaUpdRequestDto>
    {
        public CategoriaUpdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("Category id mustn't be null or zero.");

            RuleFor(x => x.Descripcion).MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"Category description can only contain up to {Miscellaneous.RegularMaxLength} characters.")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
