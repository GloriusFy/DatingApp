import { TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';

import { SignupFormService } from './signup-form.service';

describe('SignupFormService', () => {
    let service: SignupFormService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                SignupFormService,
                FormBuilder
            ]
        });
        service = TestBed.inject(SignupFormService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
