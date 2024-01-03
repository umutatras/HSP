namespace HSP.Entities;

public class Expenses:BaseEntity
{
    public int UserId { get; set; }
    public virtual CustomUser CustomUser { get; set; }
    public int CategoryId { get; set; }
    public virtual Categories Categories { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime InDate { get; set; }


}
