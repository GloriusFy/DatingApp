import { AfterViewInit, ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { SpinnerService } from './core/services/spinner/spinner.service';

@Component({
    selector: 'da-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements AfterViewInit {
    public showSpinner$: Observable<boolean> | undefined;

    constructor(private readonly spinnerService: SpinnerService) {
    }

    ngAfterViewInit(): void {
        this.showSpinner$ = this.spinnerService.showSpinner$;
    }
}