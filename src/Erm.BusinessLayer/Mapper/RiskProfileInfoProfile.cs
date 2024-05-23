using AutoMapper;

using Erm.DataAccess;

namespace Erm.BusinessLayer.Mapper;

public sealed class RiskProfileInfoProfile : Profile
{
    public RiskProfileInfoProfile()
    {
        CreateMap<RiskProfileInfo, RiskProfile>()
            .ForMember(dest => dest.RiskName,
                opt
                    => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BusinessProcess,
                opt => opt.MapFrom(src
                    => new BusinessProcess() { Name = src.BusinessProcess, Domain = src.BusinessProcess }))
            .ReverseMap()
            .ForMember(dest => dest.BusinessProcess, opt
                => opt.MapFrom(src => src.BusinessProcess.Name))
            .ForMember(dest => dest.Name, ort 
                => ort.MapFrom(src => src.RiskName));
    }
}