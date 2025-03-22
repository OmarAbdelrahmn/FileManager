using FileManager.API.Contracts.Files.Common;
using FileManager.API.Settings;
using FluentValidation;

namespace FileManager.API.Contracts.Files;

public class UpdoadFilesRequestValidator : AbstractValidator<UpdoadFilesRequest>
{
    public UpdoadFilesRequestValidator()
    {

        RuleFor(c => c.File)
            .SetValidator(new FileSizeValidator());

        RuleFor(c => c.File)
            .Must((request, context) =>
            {
                BinaryReader reader = new (request.File.OpenReadStream());

                var headerBytes = reader.ReadBytes(2);

                var fileSequenceHex = BitConverter.ToString(headerBytes);

                foreach (var signature in FileSettings.BlockedSigntures)
                    if (fileSequenceHex.Equals(fileSequenceHex , StringComparison.OrdinalIgnoreCase))                   
                        return false;
                  
                return true;

            })
            .WithMessage("Invalid file format")
            .When(c => c.File is not null);
        
    }
}
