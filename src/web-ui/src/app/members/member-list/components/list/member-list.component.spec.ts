import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { MemberListFacade } from '../../member-list.facade';

import { MemberListComponent } from './member-list.component';

describe('MemberListComponent', () => {
    let component: MemberListComponent;
    let fixture: ComponentFixture<MemberListComponent>;
    let notificationServiceSpy: jasmine.Spy;
    let memberListFacadeSpy: jasmine.Spy;

    beforeEach(async () => {
        notificationServiceSpy = jasmine.createSpyObj(`${NotificationService.name}`, ['showSuccess']);
        memberListFacadeSpy = jasmine.createSpyObj(`${MemberListFacade.name}`, {
            "getMembers": of([]),
            "getTotalMemberCount": of(0),
            "loadMembers": ''
        });

        await TestBed.configureTestingModule({
            declarations: [ MemberListComponent ],
            providers: [
                {
                    provide: NotificationService,
                    useValue: notificationServiceSpy
                },
                {
                    provide: MemberListFacade,
                    useValue: memberListFacadeSpy
                }
            ]
        })
        .overrideTemplate(MemberListComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MemberListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
