namespace Eshop_RazorPage.Models.Comment
{
    public class AddCommentCommand
    {
        public string Text { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    public class EditCommentCommand
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }

    }
}
