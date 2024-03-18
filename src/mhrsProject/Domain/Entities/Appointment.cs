using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Appointment : Entity<Guid>
{
    public Guid AppointmentTimeId { get; set; }
    public Guid HospitalId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PolyclinicId { get; set; }
    public Guid PatientId { get; set; }

    public virtual AppointmentTime? AppointmentTime { get; set; }
    public virtual Hospital? Hospital { get; set; }
    public virtual Doctor? Doctor { get; set; }
    public virtual Polyclinic? Polyclinic { get; set; }
    public virtual Patient? Patient { get; set; }

    public Appointment()
    {

    }

    public Appointment(Guid id, Guid appointmentTimeId, Guid hospitalId, Guid doctorId, Guid polyclinicId, Guid patientId) : this()
    {
        Id = id;
        AppointmentTimeId = appointmentTimeId;
        HospitalId = hospitalId;
        DoctorId = doctorId;
        PolyclinicId = polyclinicId;
        PatientId = patientId;
    }
}
