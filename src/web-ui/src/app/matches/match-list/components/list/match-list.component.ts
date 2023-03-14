import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { MatButtonToggleChange } from '@angular/material/button-toggle';
import { Observable } from 'rxjs';
import { MemberSummary } from 'src/app/core/models/member.model';
import { LikeType } from 'src/app/matches/matches.model';
import { MatchListFacade } from '../../match-list.facade';

@Component({
    selector: 'da-match-list',
    templateUrl: './match-list.component.html',
    styleUrls: ['./match-list.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MatchListComponent implements OnInit {
    public likes$: Observable<MemberSummary[]>
    public receivedLikeType = LikeType.Received;
    public putLikeType = LikeType.Put;
    public currentLikeType = LikeType.Put;

    constructor(private readonly matchListFacade: MatchListFacade) {
        this.likes$ = this.matchListFacade.getLikes();
    }

    ngOnInit(): void {
        this.matchListFacade.loadLikes(this.currentLikeType);
    }

    public onLikeTypeChange(change: MatButtonToggleChange) {
        this.currentLikeType = change.value;
        this.matchListFacade.loadLikes(change.value);
    }
}
