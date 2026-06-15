using BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAPI.Infrastructure.Data.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.Property(x => x.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasKey(x => x.TransferId);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.HasIndex(x => x.SourceAccountId);
        builder.HasIndex(x => x.DestinationAccountId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Transfers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.SourceAccount)
            .WithMany(x => x.OutgoingTransfers)
            .HasForeignKey(x => x.SourceAccountId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.DestinationAccount)
            .WithMany(x => x.IncomingTransfers)
            .HasForeignKey(x => x.DestinationAccountId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}