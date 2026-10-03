using Eshop_RazorPage.Infrastructure;
using Eshop_RazorPage.Infrastructure.JwtUtil;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.


services.AddAuthorization(option =>
{
    option.AddPolicy("Account", builder =>
    {
        builder.RequireAuthenticatedUser();
    });
    option.AddPolicy("SellerPanel", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireAssertion(s => s.User.Claims.Any
        (i => i.Type == ClaimTypes.Role && i.Value.Contains("Seller")));
    });
});

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddRazorPagesOptions(builder =>
    {
        builder.Conventions.AuthorizeFolder("/Profile", "Account");
        //builder.Conventions.AuthorizeFolder("/SellerPanel","SellerPanel");
    });



services.AddHttpContextAccessor();
services.RegisterApiServices();
services.JwtConfig(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["token"]?.ToString();

    if (!string.IsNullOrWhiteSpace(token))
        context.Request.Headers.Append("Authorization", $"Bearer {token}");

    await next();
});
app.Use(async (context, next) =>
{
    await next();

    var status = context.Response.StatusCode;
    var path = context.Request.Path;
    if (status == 401)
        context.Response.Redirect($"/Auth/Login?redirectTo={path}");
});
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
