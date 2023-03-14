import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';

import { MemberResolver } from './member.resolver';
import { MemberService } from './member.service';

describe('MemberResolver', () => {
    let resolver: MemberResolver;
    let memberServiceSpy: jasmine.Spy;

    beforeEach(() => {
        memberServiceSpy = jasmine.createSpyObj(`${MemberService.name}`, {
            "get": of({})
        })

        TestBed.configureTestingModule({
            providers: [
                MemberResolver,
                {
                    provide: MemberService,
                    useValue: memberServiceSpy
                }
            ]
        });
        resolver = TestBed.inject(MemberResolver);
    });

    it('should be created', () => {
        expect(resolver).toBeTruthy();
    });
});
