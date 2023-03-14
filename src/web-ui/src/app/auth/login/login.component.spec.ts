import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { UserService } from 'src/app/core/services/user/user.service';

import { LoginComponent } from './login.component';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    let userServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        userServiceSpy = jasmine.createSpyObj(`${UserService.name}`, {
            "login": of({})
        });

        await TestBed.configureTestingModule({
            imports: [
                RouterTestingModule
            ],
            providers: [
                {
                    provide: UserService,
                    useValue: userServiceSpy
                }
            ],
            declarations: [ LoginComponent ]
        })
        .overrideTemplate(LoginComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
