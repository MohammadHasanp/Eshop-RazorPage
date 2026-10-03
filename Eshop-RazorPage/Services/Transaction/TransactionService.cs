using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Trasaction;

namespace Eshop_RazorPage.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _client;
        public TransactionService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult<string>> CreateTRansaction(CreateTransactionCommand command)
        {
            var result = await _client.PostAsJsonAsync("Transaction", command);
            return await result.Content.ReadFromJsonAsync<ApiResult<string>>();
        }
    }
}
