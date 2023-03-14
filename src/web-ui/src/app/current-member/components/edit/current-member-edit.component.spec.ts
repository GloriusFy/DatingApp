import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { of } from 'rxjs';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { CurrentMemberFacade } from '../../current-member.facade';

import { CurrentMemberEditComponent } from './current-member-edit.component';

describe('CurrentMemberEditComponent', () => {
    let component: CurrentMemberEditComponent;
    let fixture: ComponentFixture<CurrentMemberEditComponent>;
    let currentMemberFacadeSpy: jasmine.Spy;
    let notificationServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        currentMemberFacadeSpy = jasmine.createSpyObj(`${CurrentMemberFacade.name}`, {
           "getCurrentMember": of(null),
           "getPhotoUploadingProgress": of(1),
           "loadCurrentMember": ''
        });

        notificationServiceSpy = jasmine.createSpyObj(`${NotificationService.name}`, {
            "showSuccess": ''
        });

        await TestBed.configureTestingModule({
            declarations: [ CurrentMemberEditComponent ],
            providers: [
                {
                    provide: CurrentMemberFacade,
                    useValue: currentMemberFacadeSpy
                },
                {
                    provide: NotificationService,
                    useValue: notificationServiceSpy
                },
                FormBuilder
            ]
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(CurrentMemberEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
