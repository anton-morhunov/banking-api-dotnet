using BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAPI.Infrastructure.Data.Configurations;

public class DepostConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.Property(x => x.Amount)
            .HasDefaultValue(0)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasKey(x => x.DepositId);
        
        builder.Property(x => x.DepositId)
            .ValueGeneratedNever();

        builder.Property(x => x.Description)
            .HasMaxLength(500);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}