using ExoticsCarsStoreServerSide.API.Extensions;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using ExoticsCarsStoreServerSide.Persistence.Data.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.Repository;
using ExoticsCarsStoreServerSide.Services.Mapping;
using ExoticsCarsStoreServerSide.Services.Services;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

builder.Services.AddSingleton<IConnectionMultiplexer>(SP => 
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
});

//builder.Services.AddAutoMapper(P => P.AddProfile<ProductProfile>());
//builder.Services.AddAutoMapper(A =>A.LicenseKey ="",typeof(ProductProfile).Assembly);
//14 AutoMapper
builder.Services.AddAutoMapper(Mapping =>
{
    Mapping.AddProfile(new ProductProfile());
    Mapping.AddProfile(new BasketProfile());
});
builder.Services.AddTransient<ProductPictureUrlResolver>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IDataInitializer,DataInitializer>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();

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
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
