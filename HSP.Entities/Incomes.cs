namespace HSP.Entities;

public class Incomes:BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime InDate { get; set; }
}
