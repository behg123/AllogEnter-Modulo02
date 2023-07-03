using MediatR;

namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand : IRequest<UpdateAuthorCommandResponse>
{
	public int AuthorId { get; set; }
   	public string Name {get; set;} = string.Empty;
   	public string Cpf {get; set;} = string.Empty;
}