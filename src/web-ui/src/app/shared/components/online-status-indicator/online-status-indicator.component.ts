import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
    selector: 'da-online-status-indicator',
    templateUrl: './online-status-indicator.component.html',
    styleUrls: ['./online-status-indicator.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class OnlineStatusIndicatorComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {
    }

}
