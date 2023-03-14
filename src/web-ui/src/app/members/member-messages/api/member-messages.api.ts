import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { from, Observable, of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ThreadMessage } from '../models/member-messages.model';

@Injectable()
export class MemberMessagesApi {
    private readonly hubUrl = environment.hubUrl;
    private hubConnection: HubConnection | undefined;
    private newMessage$ = new Subject<ThreadMessage>();
    private connectionEstablished: Promise<void> | undefined;

    public startConnection() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(this.hubUrl + "messages")
            .withAutomaticReconnect()
            .build();

        this.connectionEstablished = this.hubConnection.start();

        this.hubConnection.on("ReceiveNewMessage", (message: ThreadMessage) => {
            console.log(message);
            this.newMessage$.next(message);
        });
    }

    public stopConnection() {
        if (this.hubConnection) {
            this.hubConnection.stop();
        }
    }

    public getNewMessage(): Observable<ThreadMessage> {
        return this.newMessage$.asObservable();
    }

    public loadMessages(chatBuddyId: number): Observable<ThreadMessage[]> {
        const messageThread = new Subject<ThreadMessage[]>();

        if (!this.connectionEstablished) {
            return of([]);
        }

        this.connectionEstablished.then(() => {
            this.hubConnection!.invoke<ThreadMessage[]>("GetMessageThread", chatBuddyId)
                    .then(messages => messageThread.next(messages));
        });

        return messageThread.asObservable();
    }

    public sendMessage(chatBuddyId: number, text: string): Observable<ThreadMessage> {
        return from(this.hubConnection!.invoke<ThreadMessage>("SendMessage", {
            recipientId: chatBuddyId,
            content: text
        }))
    }
}
