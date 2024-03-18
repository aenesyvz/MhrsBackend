using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.AppointmentTimeId).HasColumnName("AppointmentTimeId");
        builder.Property(a => a.HospitalId).HasColumnName("HospitalId");
        builder.Property(a => a.DoctorId).HasColumnName("DoctorId");
        builder.Property(a => a.PolyclinicId).HasColumnName("PolyclinicId");
        builder.Property(a => a.PatientId).HasColumnName("PatientId");
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.AppointmentTime);
        builder.HasOne(a => a.Hospital);
        builder.HasOne(a => a.Doctor);
        builder.HasOne(a => a.Polyclinic);
        builder.HasOne(a => a.Patient);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}
