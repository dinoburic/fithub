using FitHub.Application.Common;

namespace FitHub.Application.Modules.Blog.Comments.Queries.List;

public sealed class ListCommentsQuery : BasePagedQuery<CommentListItemDto>
{
    public int? BlogPostId { get; init; }
    public int? UserId { get; init; }
}
