using FileManager.API.Settings;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadFilesRequestValidator : AbstractValidator<UpdoadFilesRequest>
{
    public UpdoadFilesRequestValidator()
    {
        RuleFor(c=>c.File)
            .Must((request , context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
            .WithMessage($"Max file size is {FileSettings.MaxFileSizeInMB} MB")
            .When(c=>c.File is not null);
    }
}
