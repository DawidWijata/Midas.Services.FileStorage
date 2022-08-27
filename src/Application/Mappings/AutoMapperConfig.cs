using Application.Dto;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using File = Domain.Entities.File;

namespace Application.Mappings;

public static class AutoMapperConfig
{
    private static MapperConfigurationExpression Config => GetConfig();

    private static MapperConfigurationExpression GetConfig()
    {
        var result = new MapperConfigurationExpression();

        result.CreateMap<Message, MessageDto>().ReverseMap();
        result.CreateMap<ICollection<Message>, MessageListDto>()
            .ForMember(dest => dest.Items, act => act.MapFrom(src => src))
            .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count));

        result.CreateMap<Guid, AddFileResultDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src))
            .ForMember(dest => dest.Success, act => act.MapFrom(src => src != Guid.Empty));

        return result;
    }

    public static IMapper Initialize()
    {
        return new MapperConfiguration(Config).CreateMapper();
    }
}