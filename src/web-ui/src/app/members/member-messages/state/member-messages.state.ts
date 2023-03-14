import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ThreadMessage } from '../models/member-messages.model';

@Injectable()
export class MemberMessagesState {
    private readonly messages$ = new BehaviorSubject<ReadonlyArray<ThreadMessage>>([]);

    constructor() {
    }

    public getMessageThread(): Observable<ReadonlyArray<ThreadMessage>> {
        return this.messages$.asObservable();
    }

    public setMessageThread(messages: ThreadMessage[]) {
        this.messages$.next(messages);
    }

    public addMessage(message: ThreadMessage) {
        this.messages$.next(this.messages$.value.concat(message));
    }
}