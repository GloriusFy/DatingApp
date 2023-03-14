import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { ApiService } from '../api/api.service';
import { PresenceService } from '../presence/presence.service';

import { UserService } from './user.service';

describe('UserService', () => {
    let service: UserService;
    let apiSpy: jasmine.Spy;
    let presenceServiceSpy: jasmine.Spy;

    beforeEach(() => {
        apiSpy = jasmine.createSpyObj(`${ApiService.name}`, {
            "post": of({})
        });

        presenceServiceSpy = jasmine.createSpyObj(`${PresenceService.name}`, {
            "trackUsersPresence": ''
        });

        TestBed.configureTestingModule({
            providers: [
                {
                    provide: ApiService,
                    useValue: apiSpy
                },
                {
                    provide: PresenceService,
                    useValue: presenceServiceSpy
                },
                UserService
            ]
        });
        service = TestBed.inject(UserService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
