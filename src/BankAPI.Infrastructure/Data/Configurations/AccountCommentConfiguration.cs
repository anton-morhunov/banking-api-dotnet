using BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAPI.Infrastructure.Data.Configurations;

public class AccountCommentConfiguration : IEntityTypeConfiguration<AccountComment>
{
    public void Configure(EntityTypeBuilder<AccountComment> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.AccountId);
        
        builder.HasOne(x => x.Account)
            .WithMany(x=>x.AccountComments)
            .HasForeignKey(x=>x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}