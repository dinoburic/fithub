import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostsApiService } from '../../../../api-services/blog/blog-posts-api.service';
import { CommentsApiService } from '../../../../api-services/blog/comments-api.service';
import { ReactionsApiService } from '../../../../api-services/blog/reactions-api.service';
import { GetBlogPostByIdQueryDto } from '../../../../api-services/blog/blog-posts-api.models';
import { CommentListItemDto, ListCommentsRequest } from '../../../../api-services/blog/comments-api.models';
import { ReactionSummaryDto } from '../../../../api-services/blog/reactions-api.models';
import { AuthFacadeService } from '../../../../core/services/auth/auth-facade.service';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-blog-detail',
  standalone: false,
  templateUrl: './blog-detail.component.html',
  styleUrl: './blog-detail.component.scss'
})
export class BlogDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private blogApi = inject(BlogPostsApiService);
  private commentsApi = inject(CommentsApiService);
  private reactionsApi = inject(ReactionsApiService);
  private authService = inject(AuthFacadeService);
    private toaster = inject(ToasterService);

  post: GetBlogPostByIdQueryDto | null = null;
  comments: CommentListItemDto[] = [];
  reactionSummary: ReactionSummaryDto | null = null;

  isLoading = true;
  isLoadingComments = false;
  errorMessage: string | null = null;

  newCommentContent = '';
  isSubmittingComment = false;

  ngOnInit(): void {
    const postId = Number(this.route.snapshot.paramMap.get('id'));
    if (postId) {
      this.loadPost(postId);
    } else {
      this.router.navigate(['/blog']);
    }
  }

  private loadPost(id: number): void {
    this.isLoading = true;
    this.blogApi.getById(id).subscribe({
      next: (post) => {
        this.post = post;
        this.isLoading = false;
        this.loadComments();
        this.loadReactions();
      },
      error: (err) => {
        this.errorMessage = 'Failed to load blog post';
        this.isLoading = false;
        this.toaster.error('Load post error:', err);
      }
    });
  }

  private loadComments(): void {
    if (!this.post) return;

    this.isLoadingComments = true;
    const request = new ListCommentsRequest();
    request.blogPostId = this.post.id;
    request.paging.pageSize = 50;

    this.commentsApi.list(request).subscribe({
      next: (response) => {
        this.comments = response.items;
        this.isLoadingComments = false;
      },
      error: (err) => {
        this.isLoadingComments = false;
        this.toaster.error('Load comments error:', err);
      }
    });
  }

  private loadReactions(): void {
    if (!this.post) return;

    this.reactionsApi.getSummary(this.post.id).subscribe({
      next: (summary) => {
        this.reactionSummary = summary;
      },
      error: (err) => {
        this.toaster.error('Load reactions error:', err);
      }
    });
  }

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  get currentUserId(): number | null {
    return this.authService.currentUser()?.userId ?? null;
  }

  onSubmitComment(): void {
    if (!this.post || !this.newCommentContent.trim() || !this.currentUserId) return;

    this.isSubmittingComment = true;
    this.commentsApi.create({
      blogPostId: this.post.id,
      userId: this.currentUserId,
      content: this.newCommentContent.trim()
    }).subscribe({
      next: () => {
        this.newCommentContent = '';
        this.isSubmittingComment = false;
        this.loadComments();
      },
      error: (err) => {
        this.isSubmittingComment = false;
        this.toaster.error('Submit comment error:', err);
      }
    });
  }

  onReact(type: string): void {
    if (!this.post || !this.currentUserId) return;

    this.reactionsApi.create({
      blogPostId: this.post.id,
      userId: this.currentUserId,
      type: type
    }).subscribe({
      next: () => {
        this.loadReactions();
      },
      error: (err) => {
        this.toaster.error('Reaction error:', err);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/blog']);
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  }

  formatDateTime(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  getReactionCount(type: string): number {
    return this.reactionSummary?.byType[type] || 0;
  }
}
