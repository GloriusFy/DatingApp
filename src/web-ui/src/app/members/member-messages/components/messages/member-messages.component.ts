import { Component, OnInit, ChangeDetectionStrategy, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { MemberMessagesFacade } from '../../member-messages.facade';
import { ThreadMessage } from '../../models/member-messages.model';

@Component({
    selector: 'da-member-messages',
    templateUrl: './member-messages.component.html',
    styleUrls: ['./member-messages.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [MemberMessagesFacade]
})
export class MemberMessagesComponent implements OnInit {
    @Input()
    public chatBuddyId!: number;

    @ViewChild(FormGroupDirective)
    public formGroupDirective!: FormGroupDirective;

    public form = new FormGroup({
        text: new FormControl('')
    });

    public messages$: Observable<ReadonlyArray<ThreadMessage>>;
    public currentUserId$: Observable<number>;

    constructor(private readonly facade: MemberMessagesFacade) {
        this.messages$ = this.facade.getMessages();
        this.currentUserId$ = this.facade.getCurrentUserId();
    }

    public ngOnInit(): void {
        this.facade.loadMessages(this.chatBuddyId);
    }

    public sendMessage() {
        const messageText = this.form.get("text")?.value;

        if (!messageText){
            return;
        }

        this.facade.sendMessage(this.chatBuddyId, messageText);
        this.form.reset();
        this.formGroupDirective.resetForm();
    }
}
