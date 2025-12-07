using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ExoticsCarsStoreServerSide.Persistence.Data.DataSeed
{
    public class DataInitializer(ExoticsCarsStoreDbContext _context) : IDataInitializer
    {
        public void Initialize()
        {
			try
			{
                var HasProducts = _context.Products.Any();
                var HasBrands = _context.ProductBrands.Any();
                var HasTypes = _context.ProductTypes.Any();
                if(HasProducts&& HasBrands&& HasTypes)
                    return;

                if (!HasBrands)
                    SeedDataFromJson<ProductBrand, int>("brands.json", _context.ProductBrands);
                if (!HasTypes)
                    SeedDataFromJson<ProductType,int>("types.json", _context.ProductTypes);
                _context.SaveChanges(); 
                if (!HasProducts)
                    SeedDataFromJson<Product,int>("products.json", _context.Products);
                _context.SaveChanges(); 
            }
			catch (Exception ex)
			{
				Console.WriteLine($"Error initializing data: {ex.Message}");
			}
        }


        // Helpers Methods
        private void SeedDataFromJson<T,TKey>(string FileName,DbSet<T> values) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\ExoticsCarsStoreServerSide.Persistence\Data\DataSeed\JSONFiles\" + FileName;
            if (!File.Exists(FilePath))
                throw new FileNotFoundException($"File{FileName} Is Not Exists");
              
            try
            {
                using var dataStream = File.OpenRead(FilePath);
                var data = JsonSerializer.Deserialize<List<T>>(dataStream, new JsonSerializerOptions() 
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
