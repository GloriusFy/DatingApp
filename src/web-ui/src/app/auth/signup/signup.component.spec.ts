import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormGroup } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { UserService } from 'src/app/core/services/user/user.service';
import { SignupFormService } from './form/services/signup-form.service';

import { SignupComponent } from './signup.component';

describe('SignupComponent', () => {
    let component: SignupComponent;
    let fixture: ComponentFixture<SignupComponent>;
    let userServiceSpy: jasmine.Spy;
    let signupFormServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        userServiceSpy = jasmine.createSpyObj(`${UserService.name}`, {
            "register": ''
        });

        signupFormServiceSpy = jasmine.createSpyObj(`${SignupFormService.name}`, {
            "getForm": new FormGroup({})
        });

        await TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            providers: [
                {
                    provide: UserService,
                    useValue: userServiceSpy
                }
            ],
            declarations: [ SignupComponent ]
        })
        .overrideProvider(SignupFormService, {useValue: signupFormServiceSpy})
        .overrideTemplate(SignupComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SignupComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
