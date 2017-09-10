import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import {ApiRoutes} from '../classes/api-routes.config';
import {ErrorServiceResponse, OkServiceResponse, ServiceResponse} from '../classes/service-response.class';
import 'rxjs/add/operator/do';
import {TokenData} from '../classes/token-data.class';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import {Router} from '@angular/router';
import {User} from '../classes/user-data.class';

@Injectable()
export class UserService {
  set token(value: TokenData) {
    if (value === null) {
      localStorage.removeItem('token');
    } else {
      localStorage.setItem('token', JSON.stringify(value));
    }
  }

  get token(): TokenData {
    const item: string = localStorage.getItem('token');
    if (item) {
      return JSON.parse(item);
    }

    return null;
  }

  set username(value: string) {
    if (value === null) {
      localStorage.removeItem('username');
    } else {
      localStorage.setItem('username', value);
    }
  }

  get username(): string {
    return localStorage.getItem('username');
  }

  constructor(private _http: Http, private _router: Router) {
  }

  login(login: string, password: string): Observable<ServiceResponse<void>> {
    const httpResult = this._http.post(ApiRoutes.token, {
      Username: login,
      Password: password
    }).map(d => d.json())
      .map(d => {
        if (d.isCritical) {
          console.error(d.value);
          throw new Error(d.error);
        }
        return d;
      })
      .map(data => {
        if (data.isSuccess) {
          const token = new TokenData(data.value.accessToken, data.value.expiresIn);
          this.token = token;
          this.username = login;
          return new OkServiceResponse<TokenData>(token);
        }

        return new ErrorServiceResponse(data.error);
      });

    return httpResult;
  }

  register(username: string, password: string): Observable<ServiceResponse<string>> {
    const httpResult = this._http.post(ApiRoutes.account.register, {
      Email: username,
      Password: password,
      Password2: password
    })
      .map(data => data.json())
      .map(d => {
        if (d.isCritical) {
          console.error(d.value);
          throw new Error(d.error);
        }
        return d;
      });

    return httpResult;
  }

  getUser(username?: string): Observable<User> {
    return new Observable(subscriber => {
      const user = new User();
      user.username = username ? username : this.username;
      user.bio = 'example bio';
      setTimeout(() => subscriber.next(user), 1000);
    });
  }

  isLoggedIn() {
    const token = this.token;

    if (!this.token || this.token.expiration + this.token.createdAt < (new Date().getUTCDate() / 1000)) {
      this.token = null;
      return false;
    }

    return true;
  }

  logOut() {
    this.token = null;
    this.username = null;
    this._router.navigateByUrl('/');
  }
}
