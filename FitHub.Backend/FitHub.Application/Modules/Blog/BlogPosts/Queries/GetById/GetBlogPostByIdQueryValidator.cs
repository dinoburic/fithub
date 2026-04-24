namespace FitHub.Application.Modules.Blog.BlogPosts.Queries.GetById;

public sealed class GetBlogPostByIdQueryValidator : AbstractValidator<GetBlogPostByIdQuery>
{
    public GetBlogPostByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
