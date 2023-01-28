import { Post } from './post'
import { PostFilter } from './post-filter'
import { Injectable } from '@angular/core'
import { EMPTY, Observable } from 'rxjs'
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'

const headers = new HttpHeaders().set('Accept', 'application/json')

@Injectable()
export class PostService {
    postList: Post[] = []
    api =
        'http://api-dotnet-blog-rest-api-1236-main.k8s.net/api/Posts'

    constructor(private http: HttpClient) {}

    findById(id: string): Observable<Post> {
        const url = `${this.api}/${id}`
        const params = { id: id }
        return this.http.get<Post>(url, { params, headers })
    }

    load(filter: PostFilter): void {
        this.find(filter).subscribe({
            next: (result) => {
                this.postList = result.reverse()
            },
            error: (err) => {
                console.error('error loading', err)
            },
        })
    }

    find(filter: PostFilter): Observable<Post[]> {
        const params = {
            category: filter.category,
        }

        return this.http.get<Post[]>(this.api, { params, headers })
    }

    save(entity: Post): Observable<Post> {
        let params = new HttpParams()
        let url = ''
        if (entity.id) {
            url = `${this.api}`
            params = new HttpParams().set('ID', entity.id.toString())
            return this.http.put<Post>(url, entity, { headers, params })
        } else {
            url = `${this.api}`
            return this.http.post<Post>(url, entity, { headers, params })
        }
    }

    delete(entity: Post): Observable<Post> {
        let params = new HttpParams()
        let url = ''
        if (entity.id) {
            url = `${this.api}/${entity.id.toString()}`
            params = new HttpParams().set('ID', entity.id.toString())
            return this.http.delete<Post>(url, { headers, params })
        }
        return EMPTY
    }
}
