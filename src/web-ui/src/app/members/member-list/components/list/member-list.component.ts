import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Observable } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { MemberListApi } from '../../api/member-list.api';
import { MemberFilter, MemberSummaryWithStatus, SortableField } from '../../models/member-list.model';
import { MemberListFacade } from '../../member-list.facade';

@Component({
    selector: 'da-member-list',
    templateUrl: './member-list.component.html',
    styleUrls: ['./member-list.component.css'],
    providers: [MemberListApi],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MemberListComponent implements OnInit {
    public readonly defaultPageSize = 12;
    public filter: MemberFilter = {
        sortBy: SortableField.Created
    };

    public members$: Observable<MemberSummary[]>;
    public totalMemberCount$: Observable<number>;

    public pageSizes = [this.defaultPageSize, 18];

    private currentPageSize = this.defaultPageSize;

    constructor(
        private readonly notificationService: NotificationService,
        private readonly memberListFacade: MemberListFacade
    ) {
        this.members$ = this.memberListFacade.getMembers();
        this.totalMemberCount$ = this.memberListFacade.getTotalMemberCount();
    }

    public ngOnInit(): void {
        this.memberListFacade.loadMembers(0, this.defaultPageSize, this.filter);
    }

    public onPageChange(event: PageEvent) {
        this.currentPageSize = event.pageSize;
        this.memberListFacade.loadMembers(event.pageIndex, event.pageSize, this.filter);
    }

    public onFilterChange(filter: MemberFilter) {
        this.filter = filter;
        this.memberListFacade.loadMembers(0, this.currentPageSize, this.filter);
    }

    public onMemberLike(member: MemberSummary){
        this.memberListFacade.likeMember(member.id)
            .subscribe(() => {
                this.notificationService.showSuccess(`You've liked ${member.name}`);
            });
    }
}
