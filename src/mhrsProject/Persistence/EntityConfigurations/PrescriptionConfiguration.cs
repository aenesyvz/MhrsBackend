using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescriptions").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.PatientId).HasColumnName("PatientId");
        builder.Property(p => p.HospitalId).HasColumnName("HospitalId");
        builder.Property(p => p.DoctorId).HasColumnName("DoctorId");
        builder.Property(p => p.PrescriptionType).HasColumnName("PrescriptionType");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(p => p.Patient);
        builder.HasOne(p => p.Hospital);
        builder.HasOne(p => p.Doctor);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}