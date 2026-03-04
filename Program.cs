using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using archolosDotNet.EF;
using archolosDotNet.Services.Item;
using archolosDotNet.Services.UserNS;
using archolosDotNet.Builders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(AuthBuilder.configureAuthentication)
    .AddJwtBearer(jwtOptions => AuthBuilder.configureJwtBearer(jwtOptions, builder.Configuration));

builder.Services.AddAuthorization();

// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.ReportApiVersions = true;
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ApiVersionReader = new UrlSegmentApiVersionReader();
// });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddScoped<IConsumableService, ConsumableService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IArmorService, ArmorService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMiscService, MiscService>();
builder.Services.AddSingleton<TokenProvider>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
