using FileManager.Api.Contracts.Common;
using FileManager.API.Contracts.Files.Common;
using FileManager.API.Settings;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UploadImageRequestValidator : AbstractValidator<UpdoadImagessRequest>
{
    public UploadImageRequestValidator()
    {
        RuleFor(c => c.Image)
            .SetValidator(new FileSizeValidator())
            .SetValidator(new FileNameValidator());

        RuleFor(c => c.Image)
            .Must((request, context) =>
            {
                var extension = Path.GetExtension(request.Image.FileName.ToLower());
                
                return FileSettings.AllowedImagesExtensions.Contains(extension);
            })
            .WithMessage("file extenion is not allowed ")
            .When(c => c is not null);
    }
}
