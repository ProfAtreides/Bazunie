using Meetings.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Database=planer; Data Source=localhost:3036; uid=root@localhost; Password=13579";

// Add services to the container.
builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();


/*app.MapGet("/", async (MySqlConnection connection) =>
{
    // open and use the connection here
    await connection.OpenAsync();
});*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();