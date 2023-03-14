import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { NotificationService } from '../../services/notification/notification.service';
import { UserService } from '../../services/user/user.service';

import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {
    let guard: AuthGuard;
    let notificationServiceSpy: jasmine.Spy;
    let userServiceSpy: jasmine.Spy;

    beforeEach(() => {
        notificationServiceSpy = jasmine.createSpyObj(`${NotificationService.name}`, {
            "showError": of()
        });

        userServiceSpy = jasmine.createSpyObj(`${UserService.name}`, {}, {
            "isAuthenticated$": of(false)
        });

        TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            providers: [
                AuthGuard,
                {
                    provide: NotificationService,
                    useValue: notificationServiceSpy
                },
                {
                    provide: UserService,
                    useValue: userServiceSpy
                }
            ]
        });
        guard = TestBed.inject(AuthGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });
});
