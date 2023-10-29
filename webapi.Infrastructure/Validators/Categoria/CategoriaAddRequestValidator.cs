using FluentValidation;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Categoria
{
    internal class CategoriaAddRequestValidator : AbstractValidator<CategoriaAddRequestDto>
    {
        public CategoriaAddRequestValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithSeverity(Severity.Error).WithMessage("Category name mustn't be null or empty.")
                .MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"Categoy name can only contain up to {Miscellaneous.RegularMaxLength} characters.");

            RuleFor(x => x.Descripcion).MaximumLength(Miscellaneous.DescriptionMaxLength).WithSeverity(Severity.Error).WithMessage($"Category description can only contain up to {Miscellaneous.DescriptionMaxLength} characters.")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
