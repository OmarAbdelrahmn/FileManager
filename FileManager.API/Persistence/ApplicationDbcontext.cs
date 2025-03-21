using FileManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManager.API.Persistence;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : DbContext(options)
{
    public DbSet<UploadedFile> Files { get; set; } = default!;
}
