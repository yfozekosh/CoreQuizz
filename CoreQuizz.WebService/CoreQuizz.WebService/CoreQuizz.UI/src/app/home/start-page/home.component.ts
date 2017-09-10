import {Component} from '@angular/core';

@Component({
    templateUrl: 'home.component.html',
    styleUrls: ['home.component.scss']
})
export class HomeComponent {
  isRegister = true;

  handleRegisterLink() {
    this.isRegister = true;
  }

  handleLoginLink() {
    this.isRegister = false;
  }
}
