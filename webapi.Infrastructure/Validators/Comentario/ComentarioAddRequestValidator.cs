using FluentValidation;
using webapi.Core.DTOs.Comentario.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Comentario
{
    public sealed class ComentarioAddRequestValidator : AbstractValidator<ComentarioAddRequestDto>
    {
        public ComentarioAddRequestValidator()
        {
            RuleFor(x => x.Texto).NotEmpty().WithSeverity(Severity.Error).WithMessage("Comment text mustn't be null or empty.")
                .MaximumLength(Miscellaneous.CommentTextMaxLength).WithSeverity(Severity.Warning).WithMessage($"Comment text shouldn't exceed the {Miscellaneous.CommentTextMaxLength} characters.");

            RuleFor(x => x.AutorId).NotEmpty().WithSeverity(Severity.Error).WithMessage("User id mustn't be null or zero.");

            RuleFor(x => x.ArticuloId).NotEmpty().WithSeverity(Severity.Error).WithMessage("Article id mustn't be null or zero.");
        }
    }
}
