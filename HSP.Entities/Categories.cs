namespace HSP.Entities;

public class Categories : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Expenses> Expenses { get; set; }
}
