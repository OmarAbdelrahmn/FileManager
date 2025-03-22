using FileManager.API.Contracts.Files;
using FileManager.API.Services.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class FilesController(IFileService service) : ControllerBase
{
    private readonly IFileService service = service;

    [HttpPost("upload")]

    public async Task<IActionResult> UploadfileAsync([FromForm] UpdoadFilesRequest request)
    {
        var FileId = await service.Upload(request);

        return Ok(FileId);
    }
    
    [HttpPost("upload-many")]

    public async Task<IActionResult> UploadManyfilesAsync([FromForm] UploadManyFilesRequest request)
    {
        var FilesId = await service.UploadManyFiles(request);

        return Ok(FilesId);
    }

}
