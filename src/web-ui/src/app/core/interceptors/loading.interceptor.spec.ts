import { TestBed } from '@angular/core/testing';
import { SpinnerService } from '../services/spinner/spinner.service';

import { LoadingInterceptor } from './loading.interceptor';

describe('LoadingInterceptor', () => {
    let spinnerServiceSpy: jasmine.Spy;

    beforeEach(() => {
        spinnerServiceSpy = jasmine.createSpyObj(`${SpinnerService.name}`, {
            "show": '',
            "hide": ''
        });

        TestBed.configureTestingModule({
            providers: [
                LoadingInterceptor,
                {
                    provide: SpinnerService,
                    useValue: spinnerServiceSpy
                }
            ]
        });
    });

    it('should be created', () => {
        const interceptor: LoadingInterceptor = TestBed.inject(LoadingInterceptor);
        expect(interceptor).toBeTruthy();
    });
});
