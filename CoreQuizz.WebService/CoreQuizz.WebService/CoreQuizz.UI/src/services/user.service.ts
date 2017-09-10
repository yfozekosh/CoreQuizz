import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {Subject} from 'rxjs/Subject';
import 'rxjs/add/operator/map';
import {ApiRoutes} from '../classes/api-routes.config';
import {ErrorServiceResponse, OkServiceResponse, ServiceResponse} from '../classes/service-response.class';
import 'rxjs/add/operator/do';
import {TokenData} from '../classes/token-data.class';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';

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

  constructor(private _http: Http) {
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
          const token = new TokenData(data.accessToken, data.expiresIn);
          this.token = token;
          return new OkServiceResponse<TokenData>(token);
        }

        return new ErrorServiceResponse(data.error);
      });

    return httpResult;
  }

  register(username: string, password: string): Observable<boolean> {
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

  isLoggedIn() {
    const token = this.token;

    if (this.token.expiration + this.token.createdAt < (new Date().getUTCDate() / 1000)) {
      this.token = null;
      return false;
    }

    return true;
  }
}
