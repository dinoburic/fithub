namespace FitHub.Application.Modules.Blog.Comments.Commands.Update;

public sealed class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
    }
}
