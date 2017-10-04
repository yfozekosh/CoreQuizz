import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: 'navbar.component.html',
  styleUrls: ['navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  @Output() onSignIn = new EventEmitter();
  @Output() onSignUp = new EventEmitter();
  searchText = '';

  constructor(private _userService: UserService, private _router: Router, private _route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this._route.queryParams.subscribe((params) => {
      const searchText = params['search-text'];
      if (searchText) {
        this.searchText = searchText;
      }
    });
  }

  handleLogOut() {
    this._userService.logOut();
  }

  handleSearch() {
    console.log('search! ' + this.searchText);
  }

  handleKeyPress(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this._router.navigate(['/console/search'], {
        queryParams: {'search-text': this.searchText}
      });
    }
  }
}
