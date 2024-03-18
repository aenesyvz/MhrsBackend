using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Hospital : Entity<Guid>
{
    public string Name { get; set; }
    public HospitalClassType HospitalClassType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public Guid CityId { get; set; }
    public Guid DistrictId { get; set; }


    public virtual City? City { get; set; }
    public virtual District? District { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Hospital()
    {
        Patients = new HashSet<Patient>();
        Doctors = new HashSet<Doctor>();
        Appointments = new HashSet<Appointment>();
    }


    public Hospital(Guid id, string name, HospitalClassType hospitalClassType, decimal? latitude, decimal? longitude, Guid cityId, Guid districtId) : this()
    {
        Id = id;
        Name = name;
        HospitalClassType = hospitalClassType;
        Latitude = latitude;
        Longitude = longitude;
        CityId = cityId;
        DistrictId = districtId;
    }

}