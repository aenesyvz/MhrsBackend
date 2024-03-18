using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Polyclinic : Entity<Guid>
{
    public string Name { get; set; }

    public virtual ICollection<Disease> Diseases { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }

    public Polyclinic()
    {
        Diseases = new HashSet<Disease>();
        Appointments = new HashSet<Appointment>();
    }

    public Polyclinic(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
