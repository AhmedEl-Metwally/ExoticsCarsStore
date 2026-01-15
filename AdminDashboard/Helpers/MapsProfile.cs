using AdminDashboard.ViewModels.ProductViewModels;
using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.ProductModule;

namespace AdminDashboard.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();

        }
    }
}
