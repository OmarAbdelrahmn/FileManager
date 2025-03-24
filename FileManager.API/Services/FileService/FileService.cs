using Azure.Core;
using FileManager.API.Contracts.Files;
using FileManager.API.Entities;
using FileManager.API.Persistence;

namespace FileManager.API.Services.FileService;

public class FileService(IWebHostEnvironment webHostEnvironment , ApplicationDbcontext dbcontext) : IFileService
{
    private readonly string filepath = $"{webHostEnvironment.WebRootPath}/Uploads";
    private readonly string Imageepath = $"{webHostEnvironment.WebRootPath}/Images";
    private readonly ApplicationDbcontext dbcontext = dbcontext;

    public async Task<(byte[] filecontent, string contentType, string fileName)> DownloadFileAsync(Guid Id)
    {
        var file = await dbcontext.Files.FindAsync(Id);

        if (file == null )
            return ([], string.Empty, string.Empty);

        var path = Path.Combine(filepath, file.StoredFileName);

        MemoryStream memoryStream = new();

        using FileStream fileStream = new(path, FileMode.Open);

        fileStream.CopyTo(memoryStream);

        memoryStream.Position = 0;

        return (memoryStream.ToArray() , file.ContentType , file.FileName);
    }

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

    public async Task UpoadImage(IFormFile image)
    {

        var path = Path.Combine(Imageepath, image.FileName);

        using var stream = File.Create(path);

        await image.CopyToAsync(stream);

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
