using Azure.Core;
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
       var uploadedfile = await SaveFile(request.File);


        await dbcontext.AddAsync(uploadedfile);
        await dbcontext.SaveChangesAsync();

        return uploadedfile.Id;
    }

    public async Task<IEnumerable<Guid>> UploadManyFiles(UploadManyFilesRequest request)
    {
        List<UploadedFile> files = [];

        foreach (var file in request.Files) {

            var uploadedfile = await SaveFile(file);
            files.Add(uploadedfile);
        }

        await dbcontext.AddRangeAsync(files);
        await dbcontext.SaveChangesAsync();

        return files.Select(f => f.Id).ToList();
    }

    private async Task<UploadedFile> SaveFile (IFormFile file)
    {
        var randomefilename = Path.GetRandomFileName();

        var uploadedfile = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = randomefilename,
            FileExtenstions = Path.GetExtension(file.FileName)
        };

        var path = Path.Combine(filepath, randomefilename);

        using var stream = File.Create(path);

        await file.CopyToAsync(stream);

        return uploadedfile;
    }
}
