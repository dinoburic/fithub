namespace FitHub.Application.Modules.Blog.Reactions.Queries.GetById;

public sealed class GetReactionByIdQuery : IRequest<GetReactionByIdQueryDto>
{
    public int Id { get; set; }
}
