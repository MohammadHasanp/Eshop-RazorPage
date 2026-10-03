using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Comment;

namespace Eshop_RazorPage.Services.Comment
{
    public interface ICommnetServices
    {
        Task<ApiResult> CreateComment(AddCommentCommand command);
        Task<ApiResult> EditComment(EditCommentCommand command);
        Task<ApiResult> DeleteComment(long CommentId);
        Task<ApiResult> ChangeStatus(long commentId,CommentStatus status);


        Task<CommentFilterResule?> GetCommentByFilter(CommentFilterParams @params);
        Task<List<CommentDto?>> GetProductComment(int pageId,int take,long productId);
        Task<CommentDto?> GetCommetById(long commentId);
    }
}
