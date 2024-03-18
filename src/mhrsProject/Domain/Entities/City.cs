using Core.Persistence.Repositories;

namespace Domain.Entities;
public class City : Entity<Guid>
{
    public int PlateCode { get; set; }
    public string Name { get; set; }

    public virtual ICollection<District> Districts { get; set; }

    public City()
    {
        Districts = new HashSet<District>();
    }

    public City(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
