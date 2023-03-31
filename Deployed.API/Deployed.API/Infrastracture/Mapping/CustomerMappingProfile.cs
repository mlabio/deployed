using AutoMapper;
using Deployed.Data.Services.Database.Models;
using Deployed.Models.Models;
using Deployed.Models.Response;

namespace Deployed.API.Infrastracture.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerResponseModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
