namespace Eshop_RazorPage.Models
{
    public class BasePaginate
    {
        public int EntityCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int Take { get; set; }
    }

    public class BaseFilterParam
    {
        public int PageId { get; set; } = 1;
        public int Take { get; set; } = 10;
    }

    public class BaseFilter<TData,TParam>:BasePaginate where TData :BaseDto where TParam : BaseFilterParam
    {
        public List<TData> Datas { get; set; } = new();
        public TParam FilterParams { get; set; }
    }
}
