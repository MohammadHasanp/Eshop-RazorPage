using Eshop_RazorPage.Infrastructure.RazorUtil;
using Eshop_RazorPage.Services.Auth;
using Eshop_RazorPage.Services.Banner;
using Eshop_RazorPage.Services.Category;
using Eshop_RazorPage.Services.Comment;
using Eshop_RazorPage.Services.Order;
using Eshop_RazorPage.Services.Product;
using Eshop_RazorPage.Services.Role;
using Eshop_RazorPage.Services.Seller;
using Eshop_RazorPage.Services.Seller.Inventory;
using Eshop_RazorPage.Services.Shop;
using Eshop_RazorPage.Services.Slider;
using Eshop_RazorPage.Services.User;
using Eshop_RazorPage.Services.UserAddress;

namespace Eshop_RazorPage.Infrastructure
{
    public static class RegisterDependcyServices
    {
        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            const string BaseAddress = "https://localhost:5001/api/";

            services.AddTransient<HttpClientAuthorizationDelegatingHandlers>();
            services.AddScoped<IRenderViewToString,RenderViewToString>();
            services.AddScoped<IShopServices,ShopServices>();

            services.AddAutoMapper(a =>
            {
                a.AddProfile<MapperProfile>();
            });

            services.AddHttpContextAccessor();
            services.AddHttpClient<IAuthServices, AuthServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IBannerServices, BannerServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ICommnetServices, CommentServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ICategoryServices, CategoryServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IOrderServices, OrderServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IProductServices, ProductServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ISellerServices, SellerServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IRoleServices, RoleServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ISellerServices, SellerServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IUserServices, UserServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IUserAddressservices, UserAddressServices>(httpContext =>
            {
                   httpContext.BaseAddress = new Uri(BaseAddress);
   
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ISellerInventoryService, SellerInventoryService>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);

            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<ISliderServices, SliderServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();

            services.AddHttpClient<IShopServices, ShopServices>(httpContext =>
            {
                httpContext.BaseAddress = new Uri(BaseAddress);
            }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandlers>();
            return services;
        }
    }
}
