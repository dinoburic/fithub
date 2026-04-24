namespace FitHub.Application.Modules.Blog.BlogPosts.Commands.Update;

public sealed class UpdateBlogPostCommandValidator : AbstractValidator<UpdateBlogPostCommand>
{
    public UpdateBlogPostCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}
