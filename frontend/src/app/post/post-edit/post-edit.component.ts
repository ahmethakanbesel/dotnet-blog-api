import { Component, OnInit } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router'
import { PostService } from '../post.service'
import { Post } from '../post'
import { map, switchMap } from 'rxjs/operators'
import { of } from 'rxjs'

@Component({
    selector: 'app-post-edit',
    templateUrl: './post-edit.component.html',
    styles: [
        // todo: figure out how to make width dynamic
        'form { display: flex; flex-direction: column; min-width: 500px; }',
        'form > * { width: 100% }',
    ],
})
export class PostEditComponent implements OnInit {
    id!: string
    post!: Post
    feedback: any = {}

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private postService: PostService
    ) {}

    ngOnInit() {
        this.route.params
            .pipe(
                map((p) => p['id']),
                switchMap((id) => {
                    if (id === 'new') {
                        return of(new Post())
                    }
                    return this.postService.findById(id)
                })
            )
            .subscribe({
                next: (post) => {
                    this.post = post
                    this.feedback = {}
                },
                error: (err) => {
                    this.feedback = {
                        type: 'warning',
                        message: 'Error loading',
                    }
                },
            })
    }

    save() {
        this.postService.save(this.post).subscribe({
            next: (post) => {
                this.post = post
                this.feedback = {
                    type: 'success',
                    message: 'Save was successful!',
                }
                setTimeout(async () => {
                    await this.router.navigate(['/posts'])
                }, 1000)
            },
            error: (err) => {
                this.feedback = { type: 'warning', message: 'Error saving' }
            },
        })
    }

    async cancel() {
        await this.router.navigate(['/posts'])
    }
}
