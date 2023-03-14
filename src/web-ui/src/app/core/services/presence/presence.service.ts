import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class PresenceService {
    private readonly hubUrl = environment.hubUrl;
    private hubConnection: HubConnection | undefined;
    private onlineUsers$ = new BehaviorSubject<number[]>([]);

    public trackUsersPresence() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(this.hubUrl + 'presence')
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => this.initOnlineUsers());

        this.hubConnection.on("UserConnected", (userId: string) => {
            this.onlineUsers$.next(this.onlineUsers$.value.concat(+userId));
        });

        this.hubConnection.on("UserDisconnected", (userId: string) => {
            this.onlineUsers$.next(this.onlineUsers$.value.filter(id => id !== +userId));
        });
    }

    public stopTrackingUsersPresence() {
        this.hubConnection?.stop()
            .catch(err => console.log(err));
    }

    public getOnlineUsers(): Observable<ReadonlyArray<number>> {
        return this.onlineUsers$.asObservable();
    }

    private initOnlineUsers() {
        this.hubConnection!.invoke<string[]>("getConnectedUsers")
            .then((result: string[]) => {
                this.onlineUsers$.next(result.map(v => +v));
            });
    }
}
