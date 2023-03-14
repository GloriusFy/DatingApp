import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MemberSummary } from 'src/app/core/models/member.model';
import { ApiService } from 'src/app/core/services/api/api.service';
import { LikeType } from '../../matches.model';

@Injectable()
export class MatchListApi {
    constructor(
        private readonly api: ApiService
    ) {
    }

    public getLikes(type: LikeType) {
        var params = new HttpParams()
            .append("likeType", type);

        return this.api.get<MemberSummary[]>("members/current/likes", params);
    }
}