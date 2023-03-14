import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserService } from 'src/app/core/services/user/user.service';
import { MemberMessagesApi } from './api/member-messages.api';
import { ThreadMessage } from './models/member-messages.model';
import { MemberMessagesState } from './state/member-messages.state';

@Injectable()
export class MemberMessagesFacade implements OnDestroy {
    private readonly subsription: Subscription;

    constructor (
        private readonly messageApi: MemberMessagesApi,
        private readonly state: MemberMessagesState,
        private readonly userService: UserService
    ) {
        this.messageApi.startConnection();
        this.subsription = this.messageApi.getNewMessage()
            .subscribe(message => {
                this.state.addMessage(message);
            })
    }

    ngOnDestroy(): void {
        this.subsription.unsubscribe();
        this.messageApi.stopConnection();
    }

    public getMessages(): Observable<ReadonlyArray<ThreadMessage>> {
        return this.state.getMessageThread();
    }

    public getCurrentUserId(): Observable<number> {
        return this.userService.currentUser$
            .pipe(
                map(user => user!.id)
            );
    }

    public loadMessages(chatBuddyId: number) {
        this.messageApi.loadMessages(chatBuddyId)
            .subscribe(messages => {
                this.state.setMessageThread(messages);
            });
    }

    public sendMessage(to: number, text: string) {
        this.messageApi.sendMessage(to, text)
            .subscribe(message => {
                this.state.addMessage(message);
            })
    }
}