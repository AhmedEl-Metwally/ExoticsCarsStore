using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExoticsCarsStoreServerSide.Persistence.Data.DataSeed
{
    public class DataInitializer(ExoticsCarsStoreDbContext _context) : IDataInitializer
    {
        public async Task InitializeAsync()
        {
			try
			{
                var HasProducts = await _context.Products.AnyAsync();
                var HasBrands = await _context.ProductBrands.AnyAsync();
                var HasTypes = await _context.ProductTypes.AnyAsync();
                if(HasProducts&&HasBrands&&HasTypes)
                    return;

                if (!HasBrands)
                   await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _context.ProductBrands);
                if (!HasTypes)
                    await SeedDataFromJsonAsync<ProductType,int>("types.json", _context.ProductTypes);
                await _context.SaveChangesAsync();
                if (!HasProducts)
                    await SeedDataFromJsonAsync<Product,int>("products.json", _context.Products);
                await _context.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing data: {ex.Message}");
			}
        }


        // Helpers Methods
        private async Task SeedDataFromJsonAsync<T,TKey>(string FileName,DbSet<T> values) where T : BaseEntity<TKey>
        {
            //D:\study\Web ITI\my pro\Route\Dot Net\Projects\ExoticsCarsStore\ExoticsCarsStoreServerSide.Persistence\Data\DataSeed\JSONFiles\brands.json
            var FilePath = @"..\ExoticsCarsStoreServerSide.Persistence\Data\DataSeed\JSONFiles\" + FileName;
            if (!File.Exists(FilePath))
                throw new FileNotFoundException($"File{FileName} Is Not Exists");
              
            try
            {
                using var dataStream = File.OpenRead(FilePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions() 
                {
                    PropertyNameCaseInsensitive = true
                });
                if (data is not null)
                    values.AddRange(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data from {FileName}: {ex.Message}");
                return;
            }
        }
    }
}
