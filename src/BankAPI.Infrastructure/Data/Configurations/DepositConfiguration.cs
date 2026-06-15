using BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAPI.Infrastructure.Data.Configurations;

public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasKey(x => x.DepositId);

        builder.Property(x => x.Description)
            .HasMaxLength(500);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.HasOne(x => x.Account)
            .WithMany(x => x.Deposits)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Deposits)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}