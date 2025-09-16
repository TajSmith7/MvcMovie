using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Features.Movies.Services;
using MvcMovie.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorOptions(options => options.ViewLocationExpanders.Add(new MvcMovie.Infrastructure.FeatureViewLocationExpander()));
builder.Services.AddScoped<MvcMovie.Features.Movies.Services.IMovieService, MvcMovie.Features.Movies.Services.MovieService>();



var app = builder.Build();

// Seed the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
