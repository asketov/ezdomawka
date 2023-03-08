using BLL;
using Common.Configs;
using DAL;
using ezdomawka.Middlewares.BanMiddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
var emailSection = builder.Configuration.GetSection(EmailConfig.Position);
builder.Services.Configure<EmailConfig>(emailSection);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options =>
{
    options.MaxModelBindingCollectionSize = int.MaxValue;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    {
        options.LoginPath = new PathString("/Auth/Login");
    });
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    
app.UseAuthorization();

app.UseBanMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
