using FileManager.API.Contracts.Files;
using FileManager.API.Entities;

namespace FileManager.API.Services.FileService;

public class FileService : IFileService
{
    public Task<Guid> Upload(UpdoadFilesRequest request)
    {
        var rondomefilename = Path.GetRandomFileName();

        var uploadedfile = new UploadedFile
        {
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            StoredFileName = rondomefilename,
            FileExtenstions = Path.GetExtension(request.File.FileName)
        };
    }
}
