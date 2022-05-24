using Lab1.Data;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Lab3
//builder.Services.AddSingleton<IStudent, StudentMoc>();

////Lab4
//builder.Services.AddSingleton<IStudent, StudentDB>();
//builder.Services.AddSingleton<IDepartment, DepartmentDB>();

//Lab5
builder.Services.AddScoped<IStudent, StudentDB>();
builder.Services.AddScoped<IDepartment, DepartmentDB>();
builder.Services.AddScoped<ICourse, CoursesMoc>();
builder.Services.AddDbContext<appDB>(a =>
{
    a.UseSqlServer("Server=Randa;Database=AspCourse;Trusted_Connection=True;");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
