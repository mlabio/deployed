using AutoMapper;
using Deployed.Data.Services.Database.Models;
using Deployed.Models.Models;
using Deployed.Models.Response;

namespace Deployed.API.Infrastracture.Mapping
{
    public class DependentMappingProfile : Profile
    {
        public DependentMappingProfile()
        {
            CreateMap<Dependent, DependentResponseModel>().ReverseMap();
            CreateMap<Dependent, DependentModel>().ReverseMap();
        }
    }
}
