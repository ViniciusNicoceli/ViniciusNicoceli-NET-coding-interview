using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Services;
using SecureFlight.Infrastructure;
using SecureFlight.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SecureFlight API",
        Version = "v1",
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<SecureFlightDbContext>(options => options.UseInMemoryDatabase("SecureFlight"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(BaseService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        using (var context = scope.ServiceProvider.GetRequiredService<SecureFlightDbContext>())
        {
            await context.Database.EnsureCreatedAsync();
        }
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
await app.RunAsync();