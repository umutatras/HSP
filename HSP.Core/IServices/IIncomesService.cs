using HSP.Core.Dtos.Incomes;

namespace HSP.Core.IServices;

public interface IIncomesService
{
    string Add(IncomesAddDto dto);
    List<IncomesListDto> GetIncomes();
    void Delete(int id);
    IncomesListDto Update(IncomesUpdateDto dto);
}
