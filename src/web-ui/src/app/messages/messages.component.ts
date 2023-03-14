import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
    selector: 'da-messages',
    templateUrl: './messages.component.html',
    styleUrls: ['./messages.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessagesComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {
    }

}
