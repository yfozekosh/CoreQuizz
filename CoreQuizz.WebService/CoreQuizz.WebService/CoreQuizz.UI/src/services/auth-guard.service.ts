import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {UserService} from './user.service';


@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private _userService: UserService, private router: Router) {
  }

  canActivate() {
    const isLoggedIn = this._userService.isLoggedIn();
    if (!isLoggedIn) {
      this.router.navigateByUrl('/?type=login');
    }
    return isLoggedIn;
  }

}
