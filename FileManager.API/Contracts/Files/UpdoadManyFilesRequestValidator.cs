using FileManager.Api.Contracts.Common;
using FileManager.API.Contracts.Files.Common;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadManyFilesRequestValidator : AbstractValidator<UploadManyFilesRequest>
{
    public UpdoadManyFilesRequestValidator()
    {
        RuleForEach(c => c.Files)
            .SetValidator(new FileSizeValidator())
            .SetValidator(new BlockedFileExtentionValidator())
            .SetValidator(new FileNameValidator());

    }
}
