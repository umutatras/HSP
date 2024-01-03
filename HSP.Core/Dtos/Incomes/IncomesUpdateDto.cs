namespace HSP.Core.Dtos.Incomes;

public class IncomesUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime InDate { get; set; }
}
