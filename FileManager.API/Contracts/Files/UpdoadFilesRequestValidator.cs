using FileManager.Api.Contracts.Common;
using FileManager.API.Contracts.Files.Common;
using FileManager.API.Settings;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadFilesRequestValidator : AbstractValidator<UpdoadFilesRequest>
{
    public UpdoadFilesRequestValidator()
    {

        RuleFor(c => c.File)
            .SetValidator(new FileSizeValidator())
            .SetValidator(new BlockedFileExtentionValidator())
            .SetValidator(new FileNameValidator());

    }
}
