using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using archolosDotNet.EF;
using archolosDotNet.Services.Item;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IConsumableService, ConsumableService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IArmorService, ArmorService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMiscService, MiscService>();

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
