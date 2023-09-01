using AspNetCoreRateLimit;
using HotelListing;
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

// Cofigure Logger (Serilog)
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "C:\\Users\\chest\\source\\repos\\HotelListing\\log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    ).CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add DB Context
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))
);
// Add Logger (Serilog)
builder.Logging.AddSerilog();
// Add Request Limiting/Throt
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimiting();
builder.Services.AddHttpContextAccessor();
// Add Response Caching
builder.Services.ConfigureHttpCacheHeaders();
// Configure Identity
builder.Services.ConfigureIdentity();
// Configure JWT Authentication
//builder.Services.AddAuthentication();
builder.Services.ConfigureJWT(builder.Configuration);
// Add CORS
builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", b =>
        b.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MapperInitializer));
// Add Unit Of Work / AuthManager
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
// Add services to the container.
builder.Services.AddControllers(config => {
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
}).AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Add API versioning
builder.Services.ConfigureVersioning();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HotelListing", Version = "v1" });
});

try
{
    Log.Information("Application Started");

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {        
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("swagger/v1/swagger.json", "HotelListing v1"));
    app.ConfigureExceptionHandler();
    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseResponseCaching();
    app.UseHttpCacheHeaders();
    app.UseIpRateLimiting();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

