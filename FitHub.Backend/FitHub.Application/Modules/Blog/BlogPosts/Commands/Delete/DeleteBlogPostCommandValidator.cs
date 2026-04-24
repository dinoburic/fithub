namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Delete;

public sealed class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
{
    public DeleteBlogPostCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
