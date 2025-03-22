using FileManager.API.Settings;
using FluentValidation;

namespace FileManager.API.Contracts.Files.Common;

public class BlockedFileExtentionValidator : AbstractValidator<IFormFile>
{
    public BlockedFileExtentionValidator()
    {
        RuleFor(c => c)
            .Must((request, context) =>
            {
                BinaryReader reader = new(request.OpenReadStream());

                var headerBytes = reader.ReadBytes(2);

                var fileSequenceHex = BitConverter.ToString(headerBytes);

                foreach (var signature in FileSettings.BlockedSigntures)
                    if (fileSequenceHex.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                        return false;

                return true;

            })
            .WithMessage("Invalid file format")
            .When(c => c is not null);
    }
}
