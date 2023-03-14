import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CoreModule } from '../../core.module';
import { NotificationService } from '../../services/notification/notification.service';
import { UserService } from '../../services/user/user.service';

@Injectable({
    providedIn: CoreModule
})
export class AuthGuard implements CanActivate {

    constructor(
        private readonly userService: UserService,
        private readonly notificationService: NotificationService,
        private readonly router: Router
    ) {
    }

    canActivate(
        _: ActivatedRouteSnapshot,
        _1: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
    {
        return this.userService.isAuthenticated$
            .pipe(
                map(isAuthenticated => {
                    if (isAuthenticated) {
                        return true;
                    }

                    this.notificationService.showError("You shall not pass!");
                    return this.router.parseUrl("/login");
                })
            )
    }
}
