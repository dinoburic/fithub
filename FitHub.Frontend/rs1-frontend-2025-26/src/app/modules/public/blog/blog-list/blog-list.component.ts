import { Component, OnInit, inject } from '@angular/core';
import { BlogPostsApiService } from '../../../../api-services/blog/blog-posts-api.service';
import { BlogPostListItemDto, ListBlogPostsRequest } from '../../../../api-services/blog/blog-posts-api.models';
import { BaseListPagedComponent } from '../../../../core/components/base-classes/base-list-paged-component';
import { Router } from '@angular/router';
import { ToasterService } from '../../../../core/services/toaster.service';

@Component({
  selector: 'app-blog-list',
  standalone: false,
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.scss'
})
export class BlogListComponent
  extends BaseListPagedComponent<BlogPostListItemDto, ListBlogPostsRequest>
  implements OnInit {

  private api = inject(BlogPostsApiService);
  private router = inject(Router);
  private toaster = inject(ToasterService);

  constructor() {
    super();
    this.request = new ListBlogPostsRequest();
  }

  ngOnInit(): void {
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();

    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) => {
        this.stopLoading('Failed to load blog posts');
        this.toaster.error('Load blog posts error:', err);
      }
    });
  }

  onViewPost(post: BlogPostListItemDto): void {
    this.router.navigate(['/blog', post.id]);
  }

  onSearch(): void {
    this.request.paging.page = 1;
    this.loadPagedData();
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  }
}
