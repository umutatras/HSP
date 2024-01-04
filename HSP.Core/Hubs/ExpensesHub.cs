using HSP.Core.IServices;
using Microsoft.AspNetCore.SignalR;

namespace HSP.Core.Hubs;

public class ExpensesHub : Hub
{
    private readonly IExpensesService _expensesService;

    public ExpensesHub(IExpensesService expensesService)
    {
        _expensesService = expensesService;
    }
    public async Task GetExpensesList()
    {
        await Clients.All.SendAsync("ReceiveExpensesList", _expensesService.GetExpenses());
    }
}
