using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Disease : Entity<Guid>
{
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }

    public virtual Polyclinic? Polyclinic { get; set; }

    public Disease()
    {

    }
    public Disease(Guid id, Guid polyclinicId, string name) : this()
    {
        Id = id;
        PolyclinicId = polyclinicId;
        Name = name;
    }
}
