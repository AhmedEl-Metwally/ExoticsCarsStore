using ExoticsCarsStoreServerSide.API.Extensions;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using ExoticsCarsStoreServerSide.Persistence.Data.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.Repository;
using ExoticsCarsStoreServerSide.Services.Mapping;
using ExoticsCarsStoreServerSide.Services.Services;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ExoticsCarsStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddAutoMapper(P => P.AddProfile<ProductProfile>());

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDataInitializer,DataInitializer>();
builder.Services.AddScoped<IProductService,ProductService>();

var app = builder.Build();

await app.MigrateSeedDatabaseAsync();
await app.SeedDatabaseAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option => 
    {
        option.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
