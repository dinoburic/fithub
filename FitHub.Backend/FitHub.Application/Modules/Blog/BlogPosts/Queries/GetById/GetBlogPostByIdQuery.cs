namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.GetById;

public sealed class GetBlogPostByIdQuery : IRequest<GetBlogPostByIdQueryDto>
{
    public int Id { get; set; }
}
