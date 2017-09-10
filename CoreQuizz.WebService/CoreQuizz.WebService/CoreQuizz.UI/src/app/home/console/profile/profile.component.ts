import {Component, Input, OnInit} from '@angular/core';
import {UserService} from '../../../../services/user.service';
import {Observable} from 'rxjs/Observable';
import {User} from '../../../../classes/user-data.class';

@Component({
  selector: 'app-profile',
  templateUrl: 'profile.component.html',
  styleUrls: ['profile.component.scss']
})
export class ProfileComponent implements OnInit {
  @Input() username;
  user: Observable<User>;

  constructor(private _userService: UserService) {

  }

  ngOnInit() {
    this.user = this._userService.getUser(this.username);
  }
}
