import {Component} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {Route, Router} from '@angular/router';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.scss']
})
export class HomeComponent {
  isRegister = true;

  constructor(private _userService: UserService, private _router: Router) {
    if (_userService.isLoggedIn()) {
      _router.navigateByUrl('/console');
    }
  }

  handleRegisterLink() {
    this.isRegister = true;
  }

  handleLoginLink() {
    this.isRegister = false;
  }
}
