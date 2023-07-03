using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Queries.GetPublisherDetail;
using Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

namespace Univali.Api.Profiles;

public class PublisherProfiles : Profile
{
    public PublisherProfiles()
    {
        CreateMap<Publisher, GetPublisherDetailDto>();
        CreateMap<Publisher, GetPublisherWithCoursesDetailDto>();
        CreateMap<Publisher, CreatePublisherDto>();
        CreateMap<Publisher, CreatePublisherCommand>().ReverseMap();
        CreateMap<Publisher, UpdatePublisherCommand>().ReverseMap();
        CreateMap<Publisher, DeletePublisherCommand>().ReverseMap();
    }
}