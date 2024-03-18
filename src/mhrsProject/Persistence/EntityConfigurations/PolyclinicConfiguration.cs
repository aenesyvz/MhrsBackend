using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PolyclinicConfiguration : IEntityTypeConfiguration<Polyclinic>
{
    public void Configure(EntityTypeBuilder<Polyclinic> builder)
    {
        builder.ToTable("Polyclinics").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(p => p.Diseases);
        builder.HasMany(p => p.Appointments);



        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}