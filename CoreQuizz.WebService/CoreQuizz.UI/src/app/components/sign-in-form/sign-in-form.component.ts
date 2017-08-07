import {Component, EventEmitter, Output} from '@angular/core';
import {ISignInFormModel} from './signInModel.interface';
import {FormControl, Validators} from '@angular/forms';
import {Router} from '@angular/router';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
    selector: 'app-sign-in-form',
    templateUrl: 'sign-in-form.component.html',
    styleUrls: ['sign-in-form.component.scss'],
    providers: []
})
export class SignInFormComponent {
    @Output() onSubmit: EventEmitter<ISignInFormModel> = new EventEmitter<ISignInFormModel>();

    public model: ISignInFormModel;
    public isSubmitted = false;

    constructor(private router: Router) {

    }

    emailFormControl = new FormControl('', [
        Validators.required,
        Validators.pattern(EMAIL_REGEX)
    ]);

    passwordFromControl = new FormControl('', [
        Validators.required
    ]);

    passwordRepeatFromControl = new FormControl('', [
        Validators.required
    ]);

    get isPasswordEqual() {
        return this.passwordFromControl.value === this.passwordRepeatFromControl.value;
    }

    handleSubmit() {
        this.isSubmitted = true;
        this.router.navigateByUrl('/main');
    }
}
