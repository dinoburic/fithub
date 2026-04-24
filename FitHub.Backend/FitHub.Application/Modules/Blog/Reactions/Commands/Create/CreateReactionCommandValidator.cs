namespace FitHub.Application.Modules.Blog.Reactions.Commands.Create;

public sealed class CreateReactionCommandValidator : AbstractValidator<CreateReactionCommand>
{
    public CreateReactionCommandValidator()
    {
        RuleFor(x => x.BlogPostId).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Type).NotEmpty().MaximumLength(50);
    }
}
