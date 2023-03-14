import { TestBed } from '@angular/core/testing';

import { PreventUnsavedFormChangesGuard } from './prevent-unsaved-form-changes.guard';

describe('PreventUnsavedFormChangesGuard', () => {
    let guard: PreventUnsavedFormChangesGuard;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                PreventUnsavedFormChangesGuard
            ]
        });
        guard = TestBed.inject(PreventUnsavedFormChangesGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });
});
