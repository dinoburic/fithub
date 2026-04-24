namespace FitHub.Application.Modules.Blog.Comments.Queries.GetById;

public sealed class GetCommentByIdQuery : IRequest<GetCommentByIdQueryDto>
{
    public int Id { get; set; }
}
