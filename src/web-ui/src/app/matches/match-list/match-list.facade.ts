import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { LikeType } from '../matches.model';
import { MatchListApi } from './api/match-list.api';

@Injectable()
export class MatchListFacade {
    private readonly likes$ = new BehaviorSubject<MemberSummary[]>([]);

    constructor(private readonly api: MatchListApi) {
    }

    public getLikes(): Observable<MemberSummary[]> {
        return this.likes$.asObservable();
    }

    public loadLikes(likeType: LikeType) {
        this.api.getLikes(likeType)
            .subscribe(likes => {
                this.likes$.next(likes);
            });
    }
}