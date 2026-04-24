namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Create;

public sealed class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
    }
}
