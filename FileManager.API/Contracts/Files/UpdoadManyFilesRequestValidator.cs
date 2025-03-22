using FileManager.API.Contracts.Files.Common;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadManyFilesRequestValidator : AbstractValidator<UploadManyFilesRequest>
{
    public UpdoadManyFilesRequestValidator()
    {
        RuleFor(c => c.Files)
            .SetValidator(new FileSizeValidator());
    }
}
