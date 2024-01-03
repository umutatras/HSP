using HSP.Core.Dtos.Expenses;

namespace HSP.Core.IServices;

public interface IExpensesService
{
    string Add(ExpensesAddDto dto);
    List<ExpensesListDto> GetExpenses();
    void Delete(int id);
    ExpensesListDto Update(ExpensesUpdateDto dto);
}
