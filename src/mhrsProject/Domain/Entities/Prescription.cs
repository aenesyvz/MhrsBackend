using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Prescription : Entity<Guid>
{

    public Guid PatientId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public PrescriptionType PrescriptionType { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Hospital? Hospital { get; set; }
    public virtual Doctor? Doctor { get; set; }
    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; }


    public Prescription()
    {
        PrescriptionDetails = new HashSet<PrescriptionDetail>();
    }
    public Prescription(Guid id, Guid patientId, Guid hospitalId, Guid doctorId, PrescriptionType prescriptionType) : this()
    {
        Id = id;
        PatientId = patientId;
        HospitalId = hospitalId;
        DoctorId = doctorId;
        PrescriptionType = prescriptionType;
    }
}
