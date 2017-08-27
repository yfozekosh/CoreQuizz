import {Component, EventEmitter, Output} from '@angular/core';
import {ISignInFormModel} from './signInModel.interface';
import {FormControl, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {UserService} from '../../../services/user.service';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
    selector: 'app-sign-in-form',
    templateUrl: 'sign-in-form.component.html',
    styleUrls: ['sign-in-form.component.scss'],
    providers: []
})
export class SignInFormComponent {
    @Output() onSubmit: EventEmitter<ISignInFormModel> = new EventEmitter<ISignInFormModel>();

    public model: ISignInFormModel = {
        email: 'te',
        password: '',
        password2: ''
    };
    public isSubmitted = false;

    constructor(private router: Router, private userService: UserService) {
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
        this.userService.login(this.emailFormControl.value, this.passwordFromControl.value).subscribe(d => console.log(`auth: ${d}`));
        this.isSubmitted = true;
        // this.router.navigateByUrl('/main');
    }
}
