using Microsoft.EntityFrameworkCore;
using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Services;
using MonolithicSPA.Models;
using MonolithicSPA.Models.Services.Contracts;
using MonolithicSPA.Models.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ثبت DbContext با اتصال به SQL Server
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data"));
});

// ثبت Repositoryها
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// ثبت Application Serviceها
builder.Services.AddScoped<IPersonApplicationService, PersonApplicationService>();
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();
builder.Services.AddScoped<ICategoryApplicationService, CategoryApplicationService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ProjectDbContext>();
        // فراخوانی کلاس سیدر
        MonolithicSPA.Models.Data.DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "خطایی در بذرپاشی اولیه دیتابیس رخ داد.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
