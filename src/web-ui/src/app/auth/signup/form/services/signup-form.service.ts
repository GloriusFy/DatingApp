import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Gender } from 'src/app/core/models/member.model';
import {
    DATE_OF_BIRTH_CONTROL_NANE,
    EMAIL_CONTROL_NAME,
    GENDER_CONTROL_NAME,
    MIN_AGE,
    MIN_PASSWORD_LENGTH,
    NAME_CONTROL_NAME,
    PASSWORD_CONFIRMATION_CONTROL_NAME,
    PASSWORD_CONTROL_NAME
} from '../form.const';
import { SignupFormValidators } from '../signup-form.validators';

@Injectable()
export class SignupFormService {
    private form: FormGroup;

    constructor(
        private readonly formBuilder: FormBuilder
    ) {
        this.form = this.formBuilder.group({
            [EMAIL_CONTROL_NAME]: this.formBuilder.control('', [
                Validators.required,
                Validators.email
            ]),
            [NAME_CONTROL_NAME]: this.formBuilder.control('', [
                Validators.required
            ]),
            [DATE_OF_BIRTH_CONTROL_NANE]: this.formBuilder.control('', [
                Validators.required,
                SignupFormValidators.minimumAgeValidator(MIN_AGE)
            ]),
            [GENDER_CONTROL_NAME]: this.formBuilder.control(Gender.Male),
            [PASSWORD_CONTROL_NAME]: this.formBuilder.control('', [
                Validators.required,
                Validators.minLength(MIN_PASSWORD_LENGTH)
            ]),
            [PASSWORD_CONFIRMATION_CONTROL_NAME]: this.formBuilder.control('', [
                Validators.required,
                SignupFormValidators.passwordConfirmationMatchPassword
            ])
        });
    }

    public getForm(): FormGroup {
        return this.form;
    }

    public emailHasErrors(): boolean {
        return this.controlHasErrors(EMAIL_CONTROL_NAME);
    }

    public getEmailError(): string | null {
        const emailControl = this.form.get(EMAIL_CONTROL_NAME)!;

        if (emailControl.hasError('required')) {
            return 'Please, enter an email';
        }

        if (emailControl.hasError('email')) {
            return 'Provide a valid email';
        }

        return null;
    }

    public nameHasErrors(): boolean {
        return this.controlHasErrors(NAME_CONTROL_NAME);
    }

    public getNameError(): string | null {
        const nameControl = this.form.get(NAME_CONTROL_NAME)!;

        if (nameControl.hasError('required')) {
            return 'Please, enter your name';
        }

        return null;
    }

    public dateOfBirthHasErrors(): boolean {
        return this.controlHasErrors(DATE_OF_BIRTH_CONTROL_NANE);
    }

    public getDateOfBirthError(): string | null {
        const nameControl = this.form.get(DATE_OF_BIRTH_CONTROL_NANE)!;

        if (nameControl.hasError('required')) {
            return 'Please, provide your date of birth';
        }

        if (nameControl.hasError('minAge')) {
            return `Minimun age requirement is ${MIN_AGE}`;
        }

        return null;
    }

    public passwordHasErrors(): boolean {
        return this.controlHasErrors(PASSWORD_CONTROL_NAME);
    }

    public getPasswordError(): string | null {
        const passwordControl = this.form.get(PASSWORD_CONTROL_NAME)!;

        if (passwordControl.hasError('required')) {
            return 'Please, enter a password';
        }

        if (passwordControl.hasError('minlength')) {
            return `Password length should be at least ${MIN_PASSWORD_LENGTH} characters`;
        }

        return null;
    }

    public passwordConfirmationHasErrors(): boolean {
        return this.controlHasErrors(PASSWORD_CONFIRMATION_CONTROL_NAME);
    }

    public getPasswordConfirmationError(): string | null {
        const passwordConfirmationControl = this.form.get(PASSWORD_CONFIRMATION_CONTROL_NAME)!;

        if (passwordConfirmationControl.hasError('required')) {
            return 'Please, enter a password confirmation';
        }

        if (passwordConfirmationControl.hasError('passwordDoNotMatch')) {
            return 'Passwords should match';
        }

        return null;
    }

    private controlHasErrors(controlName: string): boolean {
        return !!this.form.get(controlName)?.errors;
    }
}
