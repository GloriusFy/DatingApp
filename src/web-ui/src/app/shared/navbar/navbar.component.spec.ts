import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { UserService } from 'src/app/core/services/user/user.service';

import { NavBarComponent } from './navbar.component';

describe('NavBarComponent', () => {
    let component: NavBarComponent;
    let fixture: ComponentFixture<NavBarComponent>;
    let userServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        userServiceSpy = jasmine.createSpyObj(`${UserService.name}`, {}, {
            "isAuthenticated$": of(false),
            "currentUser$": of(null)
        });

        await TestBed.configureTestingModule({
            declarations: [ NavBarComponent ],
            imports: [
                RouterTestingModule.withRoutes([])
            ],
            providers: [
                {
                    provide: UserService,
                    useValue: userServiceSpy
                }
            ]
        })
        .overrideTemplate(NavBarComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(NavBarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
