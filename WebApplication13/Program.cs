using WebApplication13.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<LogActionFilter>(provider => new LogActionFilter("./Files/log.txt"));
builder.Services.AddScoped<UniqueUsersFilter>(provider => new UniqueUsersFilter("./Files/unique_users_log.txt"));

builder.Services.AddMvc(options =>
{
    options.Filters.AddService<LogActionFilter>();
    options.Filters.AddService<UniqueUsersFilter>();

});

var app = builder.Build();


app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();