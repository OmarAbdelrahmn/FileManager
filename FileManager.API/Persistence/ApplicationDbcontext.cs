using FileManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FileManager.API.Persistence;

public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : DbContext(options)
{
    public DbSet<UploadedFile> Files { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
