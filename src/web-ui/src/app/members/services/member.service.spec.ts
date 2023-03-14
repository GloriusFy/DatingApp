import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { ApiService } from 'src/app/core/services/api/api.service';

import { MemberService } from './member.service';

describe('MemberService', () => {
    let service: MemberService;
    let apiSpy: jasmine.Spy;

    beforeEach(() => {
        apiSpy = jasmine.createSpyObj(`${MemberService.name}`, {
            "get": of({})
        });

        TestBed.configureTestingModule({
            providers: [
                MemberService,
                {
                    provide: ApiService,
                    useValue: apiSpy
                }
            ]
        });
        service = TestBed.inject(MemberService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
