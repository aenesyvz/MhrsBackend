using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Doctor : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid PolyclinicId { get; set; }
    public DoctorDegreeType DoctorDegreeType { get; set; }
    public string NationalityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime DateOfBirth { get; set; }



    public virtual User User { get; set; }
    public virtual Hospital? Hospital { get; set; }
    public virtual Polyclinic? Polyclinic { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Doctor()
    {
        Patients = new HashSet<Patient>();
        Prescriptions = new HashSet<Prescription>();
        Appointments = new HashSet<Appointment>();
    }
    public Doctor(Guid id, Guid userId, Guid hospitalId, Guid polyclinicId, string nationalityNumber, DoctorDegreeType doctorDegreeType, DateTime dateOfBirth, string? ımageUrl) : this()
    {
        Id = id;
        UserId = userId;
        HospitalId = hospitalId;
        PolyclinicId = polyclinicId;
        NationalityNumber = nationalityNumber;
        DoctorDegreeType = doctorDegreeType;
        DateOfBirth = dateOfBirth;
        ImageUrl = ımageUrl;
    }
}
