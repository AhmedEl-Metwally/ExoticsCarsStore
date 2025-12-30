using ExoticsCarsStoreServerSide.API.CustomMiddleWares;
using ExoticsCarsStoreServerSide.API.Extensions;
using ExoticsCarsStoreServerSide.API.Factories;
using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using ExoticsCarsStoreServerSide.Persistence.Data.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.IdentityData.DataSeed;
using ExoticsCarsStoreServerSide.Persistence.IdentityData.DbContext;
using ExoticsCarsStoreServerSide.Persistence.Repository;
using ExoticsCarsStoreServerSide.Services.Mapping;
using ExoticsCarsStoreServerSide.Services.Services;
using ExoticsCarsStoreServerSide.ServicesAbstraction.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

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

builder.Services.AddDbContext<ExoticsCarsStoreIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(SP =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateAPiValidationResponse;
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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ICacheRepository, CacheRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddKeyedScoped<IDataInitializer, DataInitializer>("Default");
builder.Services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ExoticsCarsStoreIdentityDbContext>();

builder.Services.AddAuthentication(Config =>
{
    Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidIssuer =builder.Configuration["JwtOptions:Issuer"],
          ValidAudience =builder.Configuration["JwtOptions:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
      };
  });

var app = builder.Build();

await app.MigrateDatabaseAsync();
await app.MigrateIdentityDatabaseAsync();
await app.SeedDatabaseAsync();
await app.SeedIdentityDatabaseAsync();


// Configure the HTTP request pipeline.

//app.Use(async(Context,Next) => 
//{
//    try
//    {
//        await Next.Invoke(Context);
//    }
//    catch (Exception Ex)
//    {
//        Console.WriteLine(Ex.Message);
//        Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        await Context.Response.WriteAsJsonAsync(new 
//        {
//            StatusCode = StatusCodes.Status500InternalServerError,
//            Error = $"An unexpected error occurred: {Ex.Message}"
//        });
//    }
//});

app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
