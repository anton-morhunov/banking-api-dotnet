using BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAPI.Infrastructure.Data.Configurations;

public class ClientCommentConfiguration : IEntityTypeConfiguration<ClientComment>
{
    public void Configure(EntityTypeBuilder<ClientComment> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.ClientId);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}