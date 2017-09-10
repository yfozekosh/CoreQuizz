import {Component, EventEmitter, Output} from '@angular/core';
import {UserService} from '../../../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: 'navbar.component.html',
  styleUrls: ['navbar.component.scss']
})
export class NavbarComponent {
  @Output() onSignIn = new EventEmitter();
  @Output() onSignUp = new EventEmitter();

  constructor(private _userService: UserService) {

  }

  handleLogOut() {
    this._userService.logOut();
  }
}
