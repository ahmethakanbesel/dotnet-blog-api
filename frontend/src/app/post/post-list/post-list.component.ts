import { Component, OnInit } from '@angular/core'
import { PostFilter } from '../post-filter'
import { PostService } from '../post.service'
import { Post } from '../post'

@Component({
    selector: 'app-post',
    templateUrl: 'post-list.component.html',
    styles: [
        // todo: figure out how to make width dynamic
        'table { min-width: 1100px; margin-bottom: 48px; margin-top: 48px; }',
        'mat-cell { padding: 10px 10px 10px 10px }',
    ],
})
export class PostListComponent implements OnInit {
    displayedColumns = [
        'title',
        'category',
        'slug',
        'author',
        'coverImage',
        'createDate',
        'updatedDate',
        'actions',
    ]
    filter = new PostFilter()
    selectedPost!: Post
    feedback: any = {}

    get postList(): Post[] {
        return this.postService.postList
    }

    constructor(private postService: PostService) {}

    ngOnInit() {
        this.search()
    }

    search(): void {
        this.postService.load(this.filter)
    }

    select(selected: Post): void {
        this.selectedPost = selected
    }

    delete(post: Post): void {
        if (confirm('Are you sure?')) {
            this.postService.delete(post).subscribe({
                next: () => {
                    this.feedback = {
                        type: 'success',
                        message: 'Delete was successful!',
                    }
                    setTimeout(() => {
                        this.search()
                    }, 1000)
                },
                error: (err) => {
                    this.feedback = {
                        type: 'warning',
                        message: 'Error deleting.',
                    }
                },
            })
        }
    }
}
