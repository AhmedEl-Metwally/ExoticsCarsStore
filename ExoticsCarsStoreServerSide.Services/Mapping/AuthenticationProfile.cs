using AutoMapper;
using ExoticsCarsStoreServerSide.Domain.Models.IdentityModule;
using ExoticsCarsStoreServerSide.Shared.DTOS.OrderDTOS;

namespace ExoticsCarsStoreServerSide.Services.Mapping
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
