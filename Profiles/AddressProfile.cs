using AutoMapper;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddress;
using Univali.Api.Features.Addresses.Queries.GetAddressDetail;
using Univali.Api.Features.Addresses.Queries.GetAddressesDetail;

namespace Univali.Api.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Entities.Address, Models.AddressDto>();
        CreateMap<Entities.Address, GetAddressDetailDto>();
        CreateMap<Entities.Address, GetAddressesDetailDto>();
        CreateMap<Entities.Address, CreateAddressDto>().ReverseMap();
        CreateMap<Entities.Address, CreateAddressCommand>().ReverseMap();
        CreateMap<Entities.Address, UpdateAddressCommand>().ReverseMap();
        CreateMap<Models.AddressForUpdateDto, Entities.Address>();
        CreateMap<Models.AddressForCreationDto, Entities.Address>();

    }
}