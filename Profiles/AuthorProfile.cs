using AutoMapper;
using Univali.Api.Features.Authors.Queries.GetAuthorDetail;
using Univali.Api.Models;
using Univali.Api.Entities;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;

namespace Univali.Api.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorWithCoursesDto >().ReverseMap();
        CreateMap<Author, CreateAuthorCommand>().ReverseMap();
        CreateMap<Author, UpdateAuthorCommand>().ReverseMap();
        CreateMap<Author, GetAuthorDetailDto>().ReverseMap();
        CreateMap<Author, GetAuthorWithCoursesDetailDto>().ReverseMap();
        CreateMap<Author, CreateAuthorDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Author, AuthorIdDto>().ReverseMap();
    }
}