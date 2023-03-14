import { Component, OnInit, ChangeDetectionStrategy, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { timer } from 'rxjs';
import { debounce } from 'rxjs/operators';
import { Gender } from 'src/app/core/models/member.model';
import { MemberFilter, SortableField } from '../../models/member-list.model';

@Component({
    selector: 'da-member-filter',
    templateUrl: './member-filter.component.html',
    styleUrls: ['./member-filter.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MemberFilterComponent implements OnInit {
    @Input()
    public filter!: MemberFilter;

    @Output()
    public filterChange = new EventEmitter<MemberFilter>();

    public filterForm!: FormGroup;
    public genderList = [Gender.Male, Gender.Female];
    public sortableFieldList = [
        [SortableField.Created, "Newest members"],
        [SortableField.LastActive, "Last active"]
    ];

    constructor(private readonly formBuilder: FormBuilder) {
        this.filterForm = this.formBuilder.group({
            "minAge": this.formBuilder.control(null),
            "maxAge": this.formBuilder.control(null),
            "gender": this.formBuilder.control(null),
            "sortBy": this.formBuilder.control(null)
        });

        this.filterForm.valueChanges
            .pipe(
                debounce(() => timer(500))
            )
            .subscribe(value => {
                this.handleFormChange(value);
            });
    }

    public ngOnInit(): void {
        this.filterForm.patchValue({
            minAge: this.filter.minAge,
            maxAge: this.filter.maxAge,
            gender: this.filter.gender,
            sortBy: this.filter.sortBy
        }, {
            emitEvent: false
        });
    }

    private handleFormChange(newValue: any) {
        this.filterChange.emit({
            gender: newValue.gender,
            maxAge: newValue.maxAge,
            minAge: newValue.minAge,
            sortBy: newValue.sortBy
        });
    }
}
