using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AppointmentTime : Entity<Guid>
{
    public int Hour { get; set; }
    public int Minute { get; set; }


    public virtual ICollection<Appointment> Appointments { get; set; }

    public AppointmentTime()
    {
        Appointments = new HashSet<Appointment>();
    }

    public AppointmentTime(Guid id, int hour, int minute) : this()
    {
        Id = id;
        Hour = hour;
        Minute = minute;
    }

}