using System.ComponentModel.DataAnnotations.Schema;

namespace ExoticsCarsStoreServerSide.Domain.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [ForeignKey("ProductBrand")]
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = default!;
        [ForeignKey("ProductType")]
        public int TypeId { get; set; }
        public ProductType ProductType { get; set; } = default!;
    }
}
