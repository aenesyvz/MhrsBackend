using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors").HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.UserId).HasColumnName("UserId");
        builder.Property(d => d.HospitalId).HasColumnName("HospitalId");
        builder.Property(d => d.PolyclinicId).HasColumnName("PolyclinicId");
        builder.Property(d => d.DoctorDegreeType).HasColumnName("DoctorDegreeType");
        builder.Property(d => d.NationalityNumber).HasColumnName("NationalityNumber");
        builder.Property(d => d.FirstName).HasColumnName("FirstName");
        builder.Property(d => d.LastName).HasColumnName("LastName");
        builder.Property(d => d.DateOfBirth).HasColumnName("DateOfBirth");
        builder.Property(d => d.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(d => d.User);
        builder.HasOne(d => d.Hospital);
        builder.HasOne(d => d.Polyclinic);

        builder.HasMany(d => d.Patients);
        builder.HasMany(d => d.Prescriptions);
        builder.HasMany(d => d.Appointments);

        builder.HasQueryFilter(d => !d.DeletedDate.HasValue);
    }
}