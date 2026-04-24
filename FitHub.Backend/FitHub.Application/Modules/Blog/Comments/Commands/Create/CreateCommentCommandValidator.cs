namespace FitHub.Application.Modules.Blog.Comments.Commands.Create;

public sealed class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.BlogPostId).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
    }
}
