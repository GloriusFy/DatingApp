import { TestBed } from '@angular/core/testing';
import { MatSnackBar } from '@angular/material/snack-bar';

import { NotificationService } from './notification.service';

describe('NotificationService', () => {
    let service: NotificationService;
    let snackBarServiceSpy: jasmine.Spy;

    beforeEach(() => {
        snackBarServiceSpy = jasmine.createSpyObj(`${MatSnackBar.name}`, {
            "open": ''
        });

        TestBed.configureTestingModule({
            providers: [
                NotificationService,
                {
                    provide: MatSnackBar,
                    useValue: snackBarServiceSpy
                }
            ]
        });
        service = TestBed.inject(NotificationService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
