using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.NationalityNumber).HasColumnName("NationalityNumber");
        builder.Property(p => p.FirstName).HasColumnName("FirstName");
        builder.Property(p => p.LastName).HasColumnName("LastName");
        builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(p => p.Weight).HasColumnName("Weight");
        builder.Property(p => p.Height).HasColumnName("Height");
        builder.Property(p => p.BodyMassIndex).HasColumnName("BodyMassIndex");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(p => p.User);

        builder.HasMany(p => p.Prescriptions);
        builder.HasMany(p => p.Appointments);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}