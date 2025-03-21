using FileManager.API.Contracts.Files;
using FileManager.API.Entities;
using FileManager.API.Persistence;

namespace FileManager.API.Services.FileService;

public class FileService(IWebHostEnvironment webHostEnvironment , ApplicationDbcontext dbcontext) : IFileService
{
    private readonly string filepath = $"{webHostEnvironment.WebRootPath}/Uploads";
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<Guid> Upload(UpdoadFilesRequest request)
    {
        var randomefilename = Path.GetRandomFileName();

        var uploadedfile = new UploadedFile
        {
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            StoredFileName = randomefilename,
            FileExtenstions = Path.GetExtension(request.File.FileName)
        };

        var path = Path.Combine(filepath, randomefilename);

        using var stream =File.Create(path);

        await request.File.CopyToAsync(stream);


        await dbcontext.AddAsync(uploadedfile);
        await dbcontext.SaveChangesAsync();

        return uploadedfile.Id;
    }
}
