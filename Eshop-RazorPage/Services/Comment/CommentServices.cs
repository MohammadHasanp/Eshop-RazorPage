using Eshop_RazorPage.Infrastructure;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Comment;

namespace Eshop_RazorPage.Services.Comment
{
    public class CommentServices: ICommnetServices
    {
        private readonly HttpClient _client;
        public CommentServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResult> ChangeStatus(long commentId, CommentStatus status)
        {
            var result = await _client.PutAsJsonAsync("Comment/ChangeStatus",new {Id = commentId, status });
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> CreateComment(AddCommentCommand command)
        {
            var result = await _client.PostAsJsonAsync("comment",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> DeleteComment(long CommentId)
        {
            var result = await _client.DeleteAsync($"Comment/{CommentId}");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public async Task<ApiResult> EditComment(EditCommentCommand command)
        {
            var result = await _client.PutAsJsonAsync("Comment",command);
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }

        public Task<List<CommentDto>> GetProductComment(int pageId, int take, long productId)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentFilterResule?> GetCommentByFilter(CommentFilterParams filterParams)
        {
            var url = filterParams.GenerateBaseFilterUrl("comment");
            if (filterParams.UserId != null)
                url += $"&UserId={filterParams.UserId}";

            if (filterParams.CommentStatus != null)
                url += $"&CommentStatus={filterParams.CommentStatus}";

            if (filterParams.StartDate != null)
                url += $"&StartDate{filterParams.StartDate}";

            if (filterParams.EndDate != null)
                url += $"&EndDate{filterParams.EndDate}";
            var result = await _client.GetFromJsonAsync<ApiResult<CommentFilterResule>>(url);
            return result?.Data;
        }

        public async Task<CommentDto?> GetCommetById(long commentId)
        {
            var result = await _client.GetFromJsonAsync<ApiResult<CommentDto>>($"Comment/{commentId}");
            return result?.Data;
        }
    }
}
