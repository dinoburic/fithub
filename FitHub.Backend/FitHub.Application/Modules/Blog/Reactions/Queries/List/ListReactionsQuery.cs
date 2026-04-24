using FitHub.Application.Common;

namespace FitHub.Application.Modules.Blog.Reactions.Queries.List;

public sealed class ListReactionsQuery : BasePagedQuery<ReactionListItemDto>
{
    public int? BlogPostId { get; init; }
    public int? UserId { get; init; }
    public string? Type { get; init; }
}
