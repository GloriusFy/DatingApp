import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { FormDeactivatableComponent } from '../../components/form-deactivatable.component';
import { CoreModule } from '../../core.module';

@Injectable({
    providedIn: CoreModule
})
export class PreventUnsavedFormChangesGuard implements CanDeactivate<FormDeactivatableComponent> {
    canDeactivate(
        component: FormDeactivatableComponent,
        _1: ActivatedRouteSnapshot,
        _2: RouterStateSnapshot,
        _3?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
    {
        if (!component.canDeactivate()) {
            if (confirm("Are you sure you want to continue? Any unsaved changes will be lost")) {
                return true;
            } else {
                return false;
            }
        }

        return true;
    }
}
