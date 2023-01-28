import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing'
import { FormsModule } from '@angular/forms'
import { HttpClientTestingModule } from '@angular/common/http/testing'
import { RouterTestingModule } from '@angular/router/testing'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatIconModule } from '@angular/material/icon'
import { MatInputModule } from '@angular/material/input'
import { NoopAnimationsModule } from '@angular/platform-browser/animations'
import { PostListComponent } from './post-list.component'
import { PostService } from '../post.service'

describe('PostListComponent', () => {
    let component: PostListComponent
    let fixture: ComponentFixture<PostListComponent>

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            declarations: [PostListComponent],
            imports: [
                FormsModule,
                HttpClientTestingModule,
                RouterTestingModule,
                MatFormFieldModule,
                MatIconModule,
                MatInputModule,
                NoopAnimationsModule,
            ],
            providers: [PostService],
        }).compileComponents()
    }))

    beforeEach(() => {
        fixture = TestBed.createComponent(PostListComponent)
        component = fixture.componentInstance
        fixture.detectChanges()
    })

    it('should create', () => {
        expect(component).toBeTruthy()
    })
})
