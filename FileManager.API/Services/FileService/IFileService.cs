using FileManager.API.Contracts.Files;

namespace FileManager.API.Services.FileService;

public interface IFileService
{
    Task<Guid> Upload(UpdoadFilesRequest request);
    Task<IEnumerable<Guid>> UploadManyFiles(UploadManyFilesRequest request);
}
