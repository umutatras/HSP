namespace HSP.Core.Dtos.Expenses;

public class ExpensesAddDto
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime InDate { get; set; }

}
