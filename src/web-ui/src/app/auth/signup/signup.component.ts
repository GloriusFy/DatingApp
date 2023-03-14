import { Component, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Gender } from 'src/app/core/models/member.model';
import { UserService } from 'src/app/core/services/user/user.service';
import { DATE_OF_BIRTH_CONTROL_NANE, EMAIL_CONTROL_NAME, GENDER_CONTROL_NAME, NAME_CONTROL_NAME, PASSWORD_CONTROL_NAME } from './form/form.const';
import { SignupFormService } from './form/services/signup-form.service';

@Component({
    selector: 'da-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        SignupFormService
    ]
})
export class SignupComponent {
    public registerForm: FormGroup;
    public readonly maleGender = Gender.Male;
    public readonly femaleGender = Gender.Female;

    constructor(
        private readonly userService: UserService,
        private readonly router: Router,
        private readonly signupFormService: SignupFormService
    ) {
        this.registerForm = this.signupFormService.getForm();
    }

    public get emailHasErrors(): boolean {
        return this.signupFormService.emailHasErrors();
    }

    public get emailError(): string | null {
        return this.signupFormService.getEmailError();
    }

    public get nameHasErrors(): boolean {
        return this.signupFormService.nameHasErrors();
    }

    public get nameError(): string | null {
        return this.signupFormService.getNameError();
    }

    public get dateOfBirthHasErrors(): boolean {
        return this.signupFormService.dateOfBirthHasErrors();
    }

    public get dateOfBirthError(): string | null {
        return this.signupFormService.getDateOfBirthError();
    }

    public get passwordHasErrors(): boolean {
        return this.signupFormService.passwordHasErrors();
    }

    public get passwordError(): string | null {
        return this.signupFormService.getPasswordError();
    }

    public get passwordConfirmationHasErrors(): boolean {
        return this.signupFormService.passwordConfirmationHasErrors();
    }

    public get passwordConfirmationError(): string | null {
        return this.signupFormService.getPasswordConfirmationError();
    }

    public register() {
        const dateOfBirth = new Date(this.registerForm.get(DATE_OF_BIRTH_CONTROL_NANE)?.value);

        this.userService.register({
            email: this.registerForm.get(EMAIL_CONTROL_NAME)?.value,
            name: this.registerForm.get(NAME_CONTROL_NAME)?.value,
            dateOfBirth: dateOfBirth.toISOString().substring(0, 10),
            gender: this.registerForm.get(GENDER_CONTROL_NAME)?.value,
            password: this.registerForm.get(PASSWORD_CONTROL_NAME)?.value
        })
        .subscribe(_ => {
            this.router.navigateByUrl('/members');
        });
    }
}
