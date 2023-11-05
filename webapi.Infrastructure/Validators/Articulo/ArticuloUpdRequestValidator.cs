using FluentValidation;
using webapi.Core.DTOs.Articulo.Request;
using webapi.Core.DTOs.Categoria.Request;
using webapi.Core.StaticData;
using webapi.Infrastructure.Validators.Categoria;

namespace webapi.Infrastructure.Validators.Articulo
{
    public sealed class ArticuloUpdRequestValidator : AbstractValidator<ArticuloUpdRequestDto>
    {
        public ArticuloUpdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("Article id mustn't be null or zero.");

            RuleFor(x => x.Titulo).NotEmpty().WithSeverity(Severity.Error).WithMessage("Title must be contain some characters.")
                .MaximumLength(Miscellaneous.RegularMaxLength).WithSeverity(Severity.Error).WithMessage($"Title can only contain up to {Miscellaneous.RegularMaxLength} characters.");

            RuleFor(x => x.Contenido).NotEmpty().WithSeverity(Severity.Warning).WithMessage("The Content has to have... content, you know.");

            RuleFor(x => x.AutorId).NotEmpty().WithSeverity(Severity.Error).WithMessage("The autor id mustn't be null or zero.");

            RuleFor(x => x.Categorias).Must(IsDistinct).When(x => x.Categorias is not null && x.Categorias.Any())
                .WithSeverity(Severity.Error).WithMessage("The same category cannot be added more than once.");

            RuleForEach(x => x.Categorias).SetValidator(new CatARCRequestValidator()).When(x => x.Categorias is not null && x.Categorias.Any());
        }

        private static bool IsDistinct(ICollection<CatARCRequestDto> categories)
        {
            var idsEncontrados = new HashSet<int>();

            foreach (var category in categories)
                if (!idsEncontrados.Add(category.Id))
                    return false;

            return true;
        }
    }
}
