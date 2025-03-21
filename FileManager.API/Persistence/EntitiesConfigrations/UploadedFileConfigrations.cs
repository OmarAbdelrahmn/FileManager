using FileManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.API.Persistence.EntitiesConfigrations;

public class UploadedFileConfigrations : IEntityTypeConfiguration<UploadedFile>
{
    public void Configure(EntityTypeBuilder<UploadedFile> builder)
    {
        builder.Property(c => c.FileName)
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(c => c.ContentType)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(c => c.StoredFileName)
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(c => c.FileExtenstions)
            .HasMaxLength(10)
            .IsRequired();
        

    }
}
