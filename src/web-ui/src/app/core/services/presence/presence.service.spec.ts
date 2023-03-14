import { TestBed } from '@angular/core/testing';

import { PresenceService } from './presence.service';

describe('PresenceService', () => {
    let service: PresenceService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [PresenceService]
        });
        service = TestBed.inject(PresenceService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
