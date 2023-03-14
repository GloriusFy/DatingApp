import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { PresenceService } from 'src/app/core/services/presence/presence.service';

import { MemberDetailsComponent } from './member-details.component';

describe('MemberDetailsComponent', () => {
    let component: MemberDetailsComponent;
    let fixture: ComponentFixture<MemberDetailsComponent>;
    let presenceServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        presenceServiceSpy = jasmine.createSpyObj(`${PresenceService.name}`, {
            "getOnlineUsers": of([])
        });

        await TestBed.configureTestingModule({
            declarations: [ MemberDetailsComponent ],
            imports: [
                RouterTestingModule.withRoutes([])
            ],
            providers: [
                {
                    provide: PresenceService,
                    useValue: presenceServiceSpy
                }
            ]
        })
        .overrideTemplate(MemberDetailsComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MemberDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
