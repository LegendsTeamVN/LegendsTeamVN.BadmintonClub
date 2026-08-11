using LegendsTeamVN.Core.Domain.Aggregates;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class SportType : AggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;

    public virtual ICollection<VenueSportType> VenueSportTypes { get; private set; } = new List<VenueSportType>();
    public virtual ICollection<Court> Courts { get; private set; } = new List<Court>();

    protected SportType() { }

    public SportType(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
