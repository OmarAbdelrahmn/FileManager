namespace FileManager.API.Contracts.Files;

public record UploadManyFilesRequest
    (
    IFormFileCollection Files
    );
