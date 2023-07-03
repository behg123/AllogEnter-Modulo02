using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Features.Customers.Queries.GetCustomersDetail;
using Univali.Api.Features.Customers.Queries.GetCustomersWithAddressesDetail;
using Univali.Api.Features.Customers.Queries.GetCustomerWithAddressesDetail;


namespace Univali.Api.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // Velhos
        CreateMap<Customer, Models.CustomerDto>();
        CreateMap<Models.CustomerForUpdateDto, Customer>();
        CreateMap<Models.CustomerForCreationDto, Customer>();
        CreateMap<Customer, Models.CustomerWithAddressesDto>();

        // Novos
        CreateMap<Customer, GetCustomerDetailDto>();
        CreateMap<Customer, GetCustomersDetailDto>().ReverseMap();
        CreateMap<Customer, GetCustomerWithAddressesDetailDto>().ReverseMap();
        CreateMap<Customer, GetCustomersWithAddressesDetailDto>().ReverseMap();
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<CreateCustomerWithAddressesCommand, Customer>();
        CreateMap<Customer, CreateCustomerWithAddressesDto>();
        CreateMap<Customer, CreateCustomerDto>();
        CreateMap<UpdateCustomerCommand, Customer>();
        CreateMap<Customer, CreateCustomerCommandResponse>();
        CreateMap<Customer, CreateCustomerWithAddressesCommandResponse>().ReverseMap();
        CreateMap<UpdateCustomerWithAddressesCommand, Customer>().ReverseMap();




    }
}