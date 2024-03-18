using Core.Persistence.Repositories;

namespace Domain.Entities;

public class District : Entity<Guid>
{
    public Guid CityId { get; set; }
    public string Name { get; set; }

    public virtual City? City { get; set; }

    public District()
    {

    }

    public District(Guid id, Guid cityId, string name) : this()
    {
        Id = id;
        CityId = cityId;
        Name = name;
    }
}
