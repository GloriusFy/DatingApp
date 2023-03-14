import { TestBed } from '@angular/core/testing';
import { ApiService } from 'src/app/core/services/api/api.service';

import { MemberListApi } from './member-list.api';

describe('MemberListService', () => {
    let service: MemberListApi;
    let apiSpy: jasmine.Spy;

    beforeEach(() => {
        apiSpy = jasmine.createSpyObj(`${ApiService.name}`, ['get']);

        TestBed.configureTestingModule({
            providers: [
                MemberListApi,
                {
                    provide: ApiService,
                    useValue: apiSpy
                }
            ]
        });
        service = TestBed.inject(MemberListApi);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
