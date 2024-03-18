using Core.Persistence.Repositories;

namespace Domain.Entities;

public class PrescriptionDetail : Entity<Guid>
{
    public Guid PrescriptionId { get; set; }
    public Guid MedicineId { get; set; }
    public string Description { get; set; }
    public int Dose { get; set; }
    public int Period { get; set; }
    public string UsageType { get; set; }
    public int UsageCount { get; set; }

    public virtual Prescription? Prescription { get; set; }
    public virtual Medicine? Medicine { get; set; }

    public PrescriptionDetail()
    {

    }

    public PrescriptionDetail(Guid id, Guid prescriptionId, Guid medicineId, string description, int dose, int period, string usageType, int usageCount) : this()
    {
        Id = id;
        PrescriptionId = prescriptionId;
        MedicineId = medicineId;
        Description = description;
        Dose = dose;
        Period = period;
        UsageType = usageType;
        UsageCount = usageCount;
    }
}
