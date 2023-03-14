import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CoreModule } from '../../core.module';

@Injectable({
    providedIn: CoreModule
})
export class SpinnerService {
    private readonly showSpinner$$ = new BehaviorSubject<boolean>(false);

    public get showSpinner$(): Observable<boolean> {
        return this.showSpinner$$.asObservable();
    }

    public show() {
        this.showSpinner$$.next(true);
    }

    public hide() {
        this.showSpinner$$.next(false);
    }
}
