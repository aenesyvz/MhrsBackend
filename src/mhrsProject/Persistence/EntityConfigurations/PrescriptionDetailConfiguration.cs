using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PrescriptionDetailConfiguration : IEntityTypeConfiguration<PrescriptionDetail>
{
    public void Configure(EntityTypeBuilder<PrescriptionDetail> builder)
    {
        builder.ToTable("PrescriptionDetails").HasKey(pd => pd.Id);

        builder.Property(pd => pd.Id).HasColumnName("Id").IsRequired();
        builder.Property(pd => pd.PrescriptionId).HasColumnName("PrescriptionId");
        builder.Property(pd => pd.MedicineId).HasColumnName("MedicineId");
        builder.Property(pd => pd.Description).HasColumnName("Description");
        builder.Property(pd => pd.Dose).HasColumnName("Dose");
        builder.Property(pd => pd.Period).HasColumnName("Period");
        builder.Property(pd => pd.UsageType).HasColumnName("UsageType");
        builder.Property(pd => pd.UsageCount).HasColumnName("UsageCount");
        builder.Property(pd => pd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pd => pd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pd => pd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(pd => pd.Medicine);
        builder.HasOne(pd => pd.Prescription);
        builder.HasQueryFilter(pd => !pd.DeletedDate.HasValue);
    }
}