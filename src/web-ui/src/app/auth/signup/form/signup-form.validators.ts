import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { PASSWORD_CONTROL_NAME } from './form.const';

export class SignupFormValidators {

    public static passwordConfirmationMatchPassword(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            if (control.parent?.get(PASSWORD_CONTROL_NAME)?.value !== control.value) {
                return {"passwordDoNotMatch": true}
            }

            return null;
        }
    }

    public static minimumAgeValidator(minAge: number): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            var now = new Date();
            var birthDate = new Date(control.value);
            var age = now.getFullYear() - birthDate.getFullYear();
            var months = now.getMonth() - birthDate.getMonth();

            if (months < 0 || (months === 0 && now.getDate() < birthDate.getDate())) {
                age--;
            }

            if (age < minAge) {
                return {"minAge": true}
            }

            return null;
        }
    }
}