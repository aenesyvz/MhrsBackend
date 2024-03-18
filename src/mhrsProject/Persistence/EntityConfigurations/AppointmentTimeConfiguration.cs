using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppointmentTimeConfiguration : IEntityTypeConfiguration<AppointmentTime>
{
    public void Configure(EntityTypeBuilder<AppointmentTime> builder)
    {
        builder.ToTable("AppointmentTimes").HasKey(at => at.Id);

        builder.Property(at => at.Id).HasColumnName("Id").IsRequired();
        builder.Property(at => at.Hour).HasColumnName("Hour");
        builder.Property(at => at.Minute).HasColumnName("Minute");
        builder.Property(at => at.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(at => at.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(at => at.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(at => at.Appointments);


        builder.HasQueryFilter(at => !at.DeletedDate.HasValue);
    }
}