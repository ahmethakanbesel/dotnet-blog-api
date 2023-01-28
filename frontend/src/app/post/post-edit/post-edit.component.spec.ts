import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing'
import { FormsModule } from '@angular/forms'
import { HttpClientTestingModule } from '@angular/common/http/testing'
import { RouterTestingModule } from '@angular/router/testing'
import { MatFormFieldModule } from '@angular/material/form-field'
import { PostEditComponent } from './post-edit.component'
import { PostService } from '../post.service'

describe('PostEditComponent', () => {
    let component: PostEditComponent
    let fixture: ComponentFixture<PostEditComponent>

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            declarations: [PostEditComponent],
            imports: [
                FormsModule,
                HttpClientTestingModule,
                RouterTestingModule,
                MatFormFieldModule,
            ],
            providers: [PostService],
        }).compileComponents()
    }))

    beforeEach(() => {
        fixture = TestBed.createComponent(PostEditComponent)
        component = fixture.componentInstance
        fixture.detectChanges()
    })

    it('should create', () => {
        expect(component).toBeTruthy()
    })
})
