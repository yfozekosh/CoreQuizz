import {Component, EventEmitter, Output} from '@angular/core';
import {LoginFormInterface} from '../../../classes/login-form.interface';
import {Router} from '@angular/router';
import {UserService} from '../../../services/user.service';
import {FormControl, Validators} from '@angular/forms';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-login-form',
  templateUrl: 'login-form.component.html',
  styleUrls: ['login-form.component.scss']
})
export class LoginFormComponent {
  @Output() onSubmit: EventEmitter<any> = new EventEmitter();
  err: string;

  public model: LoginFormInterface = {
    username: '',
    password: ''
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
    this.userService
      .login(this.emailFormControl.value, this.passwordFromControl.value)
      .subscribe(d => {
        console.log('logged in');
        this.onSubmit.emit();
      });

    this.isSubmitted = true;
  }
}
