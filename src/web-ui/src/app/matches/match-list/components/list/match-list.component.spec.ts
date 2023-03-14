import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { MatchListFacade } from '../../match-list.facade';

import { MatchListComponent } from './match-list.component';

describe('MatchListComponent', () => {
    let component: MatchListComponent;
    let fixture: ComponentFixture<MatchListComponent>;
    let matchListFacadeSpy: jasmine.Spy;

    beforeEach(async () => {
        matchListFacadeSpy = jasmine.createSpyObj(`${MatchListFacade.name}`, {
            "getLikes": of([]),
            "loadLikes": ''
        });

        await TestBed.configureTestingModule({
            declarations: [ MatchListComponent ],
            providers: [
                {
                    provide: MatchListFacade,
                    useValue: matchListFacadeSpy
                }
            ]
        })
        .overrideTemplate(MatchListComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MatchListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
