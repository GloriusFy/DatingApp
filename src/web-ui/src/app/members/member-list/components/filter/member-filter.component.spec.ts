import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { SortableField } from '../../models/member-list.model';

import { MemberFilterComponent } from './member-filter.component';

describe('MemberFilterComponent', () => {
    let component: MemberFilterComponent;
    let fixture: ComponentFixture<MemberFilterComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ MemberFilterComponent ],
            providers: [
                FormBuilder
            ]
        })
        .overrideTemplate(MemberFilterComponent, '')
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MemberFilterComponent);
        component = fixture.componentInstance;
    });

    it('should create', () => {
        component.filter = {
            sortBy: SortableField.Created,
        }

        fixture.detectChanges();

        expect(component).toBeTruthy();
    });
});
