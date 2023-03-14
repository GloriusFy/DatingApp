import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { PresenceService } from 'src/app/core/services/presence/presence.service';
import { MemberListApi } from './api/member-list.api';
import { MemberFilter, MemberSummaryWithStatus } from './models/member-list.model';
import { MemberListState } from './state/member-list.state';

@Injectable()
export class MemberListFacade {
    constructor (
        private readonly api: MemberListApi,
        private readonly state: MemberListState
    ) {
    }

    public getMembers(): Observable<MemberSummary[]> {
        return this.state.getMembers();
    }

    public getTotalMemberCount(): Observable<number> {
        return this.state.getTotalMemberCount();
    }

    public loadMembers(pageIndex: number, pageSize: number, filterState: MemberFilter) {
        this.api.getMembers(pageIndex, pageSize, filterState)
            .subscribe(pagedResponse => {
                this.state.setMembers(pagedResponse);
            });
    }

    public likeMember(id: number): Observable<void> {
        return this.api.like(id);
    }
}