import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { PagedResponse } from 'src/app/core/models/pagination.model';
import { ApiService } from 'src/app/core/services/api/api.service';
import { MemberFilter } from '../models/member-list.model';

@Injectable()
export class MemberListApi {
    constructor(private readonly api: ApiService) { }

    public getMembers(pageNumber: number, pageSize: number, filter: MemberFilter)
        : Observable<PagedResponse<MemberSummary>>
    {
        let params = new HttpParams()
            .set('pageSize', pageSize.toString())
            .set('pageNumber', pageNumber.toString())
            .set('orderBy', filter.sortBy);

        if (filter.gender) {
            params = params.append('gender', filter.gender.toString());
        }

        if (filter.minAge) {
            params = params.append('minAge', filter.minAge.toString());
        }

        if (filter.maxAge) {
            params = params.append('maxAge', filter.maxAge.toString());
        }

        return this.api.get<PagedResponse<MemberSummary>>("members/list", params);
    }

    public like(targetUserId: number): Observable<void> {
        return this.api.put(`members/${targetUserId}/likes`);
    }
}
