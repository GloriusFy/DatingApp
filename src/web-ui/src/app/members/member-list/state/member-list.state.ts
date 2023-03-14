import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';

@Injectable()
export class MemberListState {
    private readonly members$ = new BehaviorSubject<MemberSummary[]>([]);
    private readonly totalMemberCount$ = new BehaviorSubject<number>(0);

    public getMembers(): Observable<MemberSummary[]> {
        return this.members$.asObservable();
    }

    public getTotalMemberCount(): Observable<number> {
        return this.totalMemberCount$.asObservable();
    }

    public setMembers(response: PagedResponse<MemberSummary>) {
        this.totalMemberCount$.next(response.totalCount);
        this.members$.next(response.items);
    }
}