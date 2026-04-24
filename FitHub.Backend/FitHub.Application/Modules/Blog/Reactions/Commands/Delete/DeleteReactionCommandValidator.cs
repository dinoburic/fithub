namespace FitHub.Application.Modules.Blog.Reactions.Commands.Delete;

public sealed class DeleteReactionCommandValidator : AbstractValidator<DeleteReactionCommand>
{
    public DeleteReactionCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
