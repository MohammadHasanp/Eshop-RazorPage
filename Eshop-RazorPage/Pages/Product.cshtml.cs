using Eshop_RazorPage.Infrastructure.Util;
using Eshop_RazorPage.Infrastructure.Util.RazorUtil;
using Eshop_RazorPage.Models;
using Eshop_RazorPage.Models.Comment;
using Eshop_RazorPage.Services.Comment;
using Eshop_RazorPage.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_RazorPage.Pages
{
    public class ProductModel : BaseRazorPage
    {
        private readonly IProductServices _services;
        private readonly ICommnetServices _commentService;
        public ProductModel(IProductServices services, ICommnetServices commentService)
        {
            _services = services;
            _commentService = commentService;
        }
        public SingleProduct ProductPageModel { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            var product = await _services.GetSingleProduct(slug);
            if (product == null)
                return NotFound();

            ProductPageModel = product;
            return Page();
        }
        public async Task<IActionResult> OnGetProductComments(long productId,int pageId = 1)
        {
            var result = await _commentService.GetProductComments(new ProdcutCommentsParams()
            {
                PageId = pageId,
                ProductId = productId,
                Take = 1, 
            });
            return Partial("Shared/Products/ProductComments",model:result);
        }
        public async Task<IActionResult> OnPost(string slug,long productId,string commnet)
        {
            if (User.Identity.IsAuthenticated == false)
                return Page();

            var result = await _commentService.CreateComment(new AddCommentCommand()
            {
                ProductId = productId,
                Text = commnet,
                UserId = User.GetUserId()
            });
            if (result.IsSuccess == false)
            {
                ErrorAlert(result.MetaData.Message);
                return Page();
            }
            SuccessAlert("نظر شما ثبت شد ، بعد از تایید در سایت نمایش داده می شود");
            return RedirectToPage("Product", new { slug });
        }
        public async Task<IActionResult>OnPostDeleteComment(long Id)
        {
            return await AjaxTryCatch(()=> _commentService.DeleteComment(Id));
        }
    }
}
