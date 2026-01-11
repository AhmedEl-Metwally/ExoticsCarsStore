using ExoticsCarsStoreServerSide.API.CustomMiddleWares;
using ExoticsCarsStoreServerSide.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddWebApplicationServices(builder.Configuration);

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

app.UseWebApplicationMiddleware();

app.Run();
