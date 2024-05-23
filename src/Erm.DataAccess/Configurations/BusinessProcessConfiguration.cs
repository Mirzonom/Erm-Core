using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erm.DataAccess;

public sealed class BusinessProcessConfiguration : IEntityTypeConfiguration<BusinessProcess>
{
    public void Configure(EntityTypeBuilder<BusinessProcess> builder)
    {
        builder.ToTable("business_process");

        builder
            .Property(p => p.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .Property(p => p.Domain)
            .HasColumnName("domain")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder
            .HasMany(p => p.RiskProfiles)
            .WithOne(p => p.BusinessProcess)
            .HasForeignKey(fk => fk.BusinessProcessId)
            .IsRequired();

        builder.HasKey(k => k.Id);
    }
}