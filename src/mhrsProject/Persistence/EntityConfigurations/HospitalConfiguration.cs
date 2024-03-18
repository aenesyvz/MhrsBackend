using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
{
    public void Configure(EntityTypeBuilder<Hospital> builder)
    {
        builder.ToTable("Hospitals").HasKey(h => h.Id);

        builder.Property(h => h.Id).HasColumnName("Id").IsRequired();
        builder.Property(h => h.Name).HasColumnName("Name");
        builder.Property(h => h.HospitalClassType).HasColumnName("HospitalClassType");
        builder.Property(h => h.Latitude).HasColumnName("Latitude");
        builder.Property(h => h.Longitude).HasColumnName("Longitude");
        builder.Property(h => h.CityId).HasColumnName("CityId");
        builder.Property(h => h.DistrictId).HasColumnName("DistrictId");
        builder.Property(h => h.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(h => h.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(h => h.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(h => h.City);
        builder.HasOne(h => h.District);

        builder.HasMany(h => h.Prescriptions);
        builder.HasMany(h => h.Patients);
        builder.HasMany(h => h.Doctors);
        builder.HasMany(h => h.Appointments);

        builder.HasQueryFilter(h => !h.DeletedDate.HasValue);
    }
}