using System.Data.SqlClient;
using TI_Devops_2024_DemoAspMvc.BLL.Interfaces;
using TI_Devops_2024_DemoAspMvc.BLL.Services;
using TI_Devops_2024_DemoAspMvc.DAL.Interfaces;
using TI_Devops_2024_DemoAspMvc.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<SqlConnection>(sp =>
{
    return new SqlConnection(builder.Configuration.GetConnectionString("default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
