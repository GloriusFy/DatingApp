import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { MemberMessagesFacade } from '../../member-messages.facade';

import { MemberMessagesComponent } from './member-messages.component';

describe('MessagesComponent', () => {
    let component: MemberMessagesComponent;
    let fixture: ComponentFixture<MemberMessagesComponent>;
    let memberMessageFacadeSpy: jasmine.Spy;

    beforeEach(async () => {
        memberMessageFacadeSpy = jasmine.createSpyObj(`${MemberMessagesFacade.name}`, {
            "getMessages": of([]),
            "getCurrentUserId": of(1),
            "loadMessages": ''
        });

        await TestBed.configureTestingModule({
            declarations: [ MemberMessagesComponent ]
        })
        .overrideProvider(MemberMessagesFacade, { useValue: memberMessageFacadeSpy})
        .overrideTemplate(MemberMessagesComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MemberMessagesComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
