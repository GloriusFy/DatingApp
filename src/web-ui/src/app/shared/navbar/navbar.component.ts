import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/core/services/user/user.model';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
    selector: 'da-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavBarComponent implements OnInit {
    public isUserAuthenticated$: Observable<boolean>;
    public user$: Observable<IUser | null>;

    constructor(
        private readonly userService: UserService,
        private readonly router: Router
    ) {
        this.isUserAuthenticated$ = this.userService.isAuthenticated$;
        this.user$ = this.userService.currentUser$;
    }

    ngOnInit(): void {
    }

    public logout() {
        this.userService.logout()
            .subscribe(() => {
                this.router.navigateByUrl("/");
            });
    }
}
