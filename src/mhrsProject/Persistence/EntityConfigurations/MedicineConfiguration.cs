using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("Medicines").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.MedicineCompanyId).HasColumnName("MedicineCompanyId");
        builder.Property(m => m.Name).HasColumnName("Name");
        builder.Property(m => m.PurposeOfUsage).HasColumnName("PurposeOfUsage");
        builder.Property(m => m.SideEffects).HasColumnName("SideEffects");
        builder.Property(m => m.ConditionsToBeConsidired).HasColumnName("ConditionsToBeConsidired");
        builder.Property(m => m.TermsOfUse).HasColumnName("TermsOfUse");
        builder.Property(m => m.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(m => m.MedicineCompany);

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
    }
}