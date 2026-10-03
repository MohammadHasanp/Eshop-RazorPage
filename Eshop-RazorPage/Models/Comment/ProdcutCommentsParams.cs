namespace Eshop_RazorPage.Models.Comment
{
    public class ProdcutCommentsParams
    {
        public long ProductId { get; set; }
        public int Take { get; set; } = 10;
        public int PageId { get; set; } = 1;
    }
}
