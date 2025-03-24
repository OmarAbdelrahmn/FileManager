using FileManager.API.Contracts.Files;

namespace FileManager.API.Services.FileService;

public interface IFileService
{
    Task<Guid> Upload(UpdoadFilesRequest request);
    Task<IEnumerable<Guid>> UploadManyFiles(UploadManyFilesRequest request);
    Task UpoadImage(IFormFile image);
    Task<(byte[] filecontent, string contentType , string fileName)> DownloadFileAsync(Guid Id);
    Task<(FileStream? fileStream, string contentType , string fileName)> FileStream(Guid Id);
}
