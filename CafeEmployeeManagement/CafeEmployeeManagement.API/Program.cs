using Autofac;
using Autofac.Extensions.DependencyInjection;
using CafeEmployeeManagement.API.Extensions.Middleware;
using CafeEmployeeManagement.Application;
using CafeEmployeeManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React app's origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterInfrastructureServices(builder.Configuration);
    containerBuilder.RegisterApplicationServices();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();
