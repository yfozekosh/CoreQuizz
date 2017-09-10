import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {Observable} from 'rxjs/Observable';

@Component({
  selector: 'app-jumbotron',
  templateUrl: 'jumbotron.component.html',
  styleUrls: ['jumbotron.component.scss']
})
export class JumbotronComponent implements OnDestroy, OnInit {
  @Input() isRegister = true;
  private _querySubscription;

  constructor(private _router: Router, private _route: ActivatedRoute) {
  }

  ngOnInit() {
    this._querySubscription = this._route.queryParams.subscribe(params => {
      const type = params['type'];
      if (type === 'login') {
        this.isRegister = false;
      }
    });
  }

  ngOnDestroy() {
    this._querySubscription.unsubscribe();
  }

  handleRegister() {
    this._router.navigateByUrl('/?type=login');
  }

  handleLogin() {
    this._router.navigateByUrl('/console');
  }
}
