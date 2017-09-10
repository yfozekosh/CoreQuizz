import {Component, Input, OnInit} from '@angular/core';
import {UserService} from '../../../services/user.service';

@Component({
  selector: 'app-authentificated',
  template: `
    <ng-container *ngIf="!isHidden">
      <ng-content></ng-content>
    </ng-container>`
})
export class AuthenticatedComponent implements OnInit {
  // straigh , inverse
  @Input() type = 'straight';

  public isHidden = false;

  constructor(private _userService: UserService) {
  }

  ngOnInit(): void {
    if (this.type === 'inverse') {
      this.isHidden = this._userService.isLoggedIn();
    } else if (this.type === 'straight') {
      this.isHidden = !this._userService.isLoggedIn();
    }
  }
}
