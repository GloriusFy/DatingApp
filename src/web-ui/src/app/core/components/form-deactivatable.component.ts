import { FormGroup } from '@angular/forms';
import { DeactivatableComponent } from './deactivatable.component';

export abstract class FormDeactivatableComponent extends DeactivatableComponent {
    abstract get form(): FormGroup;

    canDeactivate(): boolean {
        return this.form.pristine;
    }
}