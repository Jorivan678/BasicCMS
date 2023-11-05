using FluentValidation;
using webapi.Core.DTOs.Categoria.Request;

namespace webapi.Infrastructure.Validators.Categoria
{
    public sealed class CatARCRequestValidator : AbstractValidator<CatARCRequestDto>
    {
        public CatARCRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("Category id mustn't be null or zero.");
        }
    }
}
