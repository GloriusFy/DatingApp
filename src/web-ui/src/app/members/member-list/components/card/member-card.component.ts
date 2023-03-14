import { Component, OnInit, ChangeDetectionStrategy, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MemberSummary } from 'src/app/core/models/member.model';
import { PresenceService } from 'src/app/core/services/presence/presence.service';

@Component({
    selector: 'da-member-card',
    templateUrl: './member-card.component.html',
    styleUrls: ['./member-card.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MemberCardComponent implements OnInit {
    @Input()
    public member!: MemberSummary;

    @Output()
    public memberLike = new EventEmitter<void>();

    public online$: Observable<boolean>;

    constructor(private readonly presenceService: PresenceService) {
        this.online$ = this.presenceService.getOnlineUsers()
            .pipe(
                map(onlineMembers => !!onlineMembers.includes(this.member.id))
            );
    }

    ngOnInit(): void {
    }

    public onMemberLikeClick() {
        this.memberLike.emit();
    }
}
