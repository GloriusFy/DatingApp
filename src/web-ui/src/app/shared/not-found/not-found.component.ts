import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'da-not-found',
    templateUrl: './not-found.component.html',
    styleUrls: ['./not-found.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class NotFoundComponent {
}
