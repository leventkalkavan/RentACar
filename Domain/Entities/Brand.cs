using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Brand : BaseEntity<Guid>
{
    public Brand()
    {
        Models = new HashSet<Model>();
    }

    public Brand(Guid id, string name) : this()
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
    public virtual ICollection<Model> Models { get; set; }
}