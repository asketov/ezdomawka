using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Common.Configs;
using DAL;
using DAL.Extensions;
using ezdomawka.Middlewares.BanMiddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var emailSection = builder.Configuration.GetSection(EmailConfig.Position);
builder.Services.Configure<EmailConfig>(emailSection);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEmailSender, LocalEmailSender>(provider =>
    {
        return new LocalEmailSender((message) => Console.WriteLine(message));
    });
}
else
{
    builder.Services.AddSingleton<IEmailSender, EmailSender>();
}

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

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<DataContext>();

        if (!await dbContext.BaseUserRolesAdded())
            await dbContext.AddBaseUserRoles();

        if (!await dbContext.AnySubjectAdded())
            await dbContext.AddSubjectFromFile(@"D:\lesons\DotnetMicrosoftGuidLearningProjects\MAIN\Tasks\ezdomawka\ezdomawka\предметы.txt");
        
        if(!await dbContext.AnyThemeAdded())
            await dbContext.AddThemeFromFile(@"D:\lesons\DotnetMicrosoftGuidLearningProjects\MAIN\Tasks\ezdomawka\ezdomawka\темы.txt");

        
        dbContext.SaveChanges();
    }
}
    
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
