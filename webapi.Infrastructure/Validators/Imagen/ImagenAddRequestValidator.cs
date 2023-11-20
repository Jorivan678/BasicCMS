using FluentValidation;
using Microsoft.AspNetCore.Http;
using webapi.Core.DTOs.Imagen.Request;
using webapi.Core.StaticData;

namespace webapi.Infrastructure.Validators.Imagen
{
    public sealed class ImagenAddRequestValidator : AbstractValidator<ImageAddRequestDto<IFormFile>>
    {
        public ImagenAddRequestValidator()
        {
            RuleFor(x => x.RequestFile).NotNull().WithSeverity(Severity.Error).WithMessage("Image file cannot be null.");

            RuleFor(x => x.Titulo).NotEmpty().WithSeverity(Severity.Error).WithMessage("The title cannot be null or empty.")
                .MaximumLength(Miscellaneous.ImageTitleMaxLength).WithSeverity(Severity.Error).WithMessage($"The title cannot exceed {Miscellaneous.ImageTitleMaxLength} characters.");

            RuleFor(x => x.UsuarioId).NotEmpty().WithSeverity(Severity.Error).WithMessage("The user id cannot be zero or empty.");
        }
    }
}