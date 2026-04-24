namespace FitHub.Application.Modules.Blog.Comments.Commands.Delete;

public sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
