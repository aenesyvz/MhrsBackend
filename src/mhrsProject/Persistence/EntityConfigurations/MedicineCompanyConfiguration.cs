using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MedicineCompanyConfiguration : IEntityTypeConfiguration<MedicineCompany>
{
    public void Configure(EntityTypeBuilder<MedicineCompany> builder)
    {
        builder.ToTable("MedicineCompanies").HasKey(mc => mc.Id);

        builder.Property(mc => mc.Id).HasColumnName("Id").IsRequired();
        builder.Property(mc => mc.Name).HasColumnName("Name");
        builder.Property(mc => mc.Address).HasColumnName("Address");
        builder.Property(mc => mc.Email).HasColumnName("Email");
        builder.Property(mc => mc.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(mc => mc.TaxOffice).HasColumnName("TaxOffice");
        builder.Property(mc => mc.TaxNumber).HasColumnName("TaxNumber");
        builder.Property(mc => mc.ImageUrl).HasColumnName("ImageUrl");
        builder.Property(mc => mc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(mc => mc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(mc => mc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(mc => mc.Medicines);

        builder.HasQueryFilter(mc => !mc.DeletedDate.HasValue);
    }
}
