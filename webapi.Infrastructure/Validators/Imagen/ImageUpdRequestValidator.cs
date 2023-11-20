using FluentValidation;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Imagen
{
    public sealed class ImageUpdRequestValidator : AbstractValidator<ImageUpdRequestDto>
    {
        public ImageUpdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithSeverity(Severity.Error).WithMessage("The image id cannot be zero or empty.");

            RuleFor(x => x.Titulo).NotEmpty().WithSeverity(Severity.Error).WithMessage("The title cannot be null or empty.")
                .MaximumLength(Miscellaneous.ImageTitleMaxLength).WithSeverity(Severity.Error).WithMessage($"The title cannot exceed {Miscellaneous.ImageTitleMaxLength} characters.");
        }
    }
}