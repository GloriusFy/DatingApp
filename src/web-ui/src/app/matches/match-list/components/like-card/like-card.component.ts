import { Component, ChangeDetectionStrategy, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MemberSummary } from 'src/app/core/models/member.model';
import { PresenceService } from 'src/app/core/services/presence/presence.service';

@Component({
    selector: 'da-like-card',
    templateUrl: './like-card.component.html',
    styleUrls: ['./like-card.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LikeCardComponent {
    @Input()
    public likedMember!: MemberSummary;

    public online$: Observable<boolean>;

    constructor(private readonly presenceService: PresenceService) {
        this.online$ = this.presenceService.getOnlineUsers()
            .pipe(
                map(onlineMembers => !!onlineMembers.includes(this.likedMember.id))
            );
    }
}
