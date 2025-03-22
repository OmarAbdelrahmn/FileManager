using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadFilesRequestValidator : AbstractValidator<UpdoadFilesRequest>
{
    public UpdoadFilesRequestValidator()
    {
        RuleFor()
    }
}
