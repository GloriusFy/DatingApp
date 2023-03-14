import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { SpinnerService } from '../services/spinner/spinner.service';
import { finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    private reqCount = 0;
    constructor(private readonly spinnerService: SpinnerService) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        if (this.reqCount === 0) {
            this.spinnerService.show();
            this.reqCount++;
        }

        return next.handle(request)
            .pipe(
                finalize(() => {
                    if (this.reqCount === 1) {
                        this.spinnerService.hide();
                        this.reqCount--;
                    }
                })
            );
    }
}
