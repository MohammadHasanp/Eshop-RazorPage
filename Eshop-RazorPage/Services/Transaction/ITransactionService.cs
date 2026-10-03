using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Trasaction;

namespace Eshop_RazorPage.Services.Transaction
{
    public interface ITransactionService
    {
        Task<ApiResult<string>> CreateTRansaction(CreateTransactionCommand command);
    }
}
