import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import {Subject} from 'rxjs/Subject';
import 'rxjs/add/operator/map';

interface ILoginRequest {
    login: string;
    password: string;
}


interface ILoginResponse {
    token: string;
    error: string;
}

@Injectable()
export class UserService {
    private token: string;

    constructor(private _http: Http) {
    }

    login(login: string, password: string): Observable<boolean> {
        const res = new Subject<boolean>();

        const loginObservable = this._http.post('http://localhost:8080/api/access/user', JSON.stringify({
            login: login,
            password: password
        })).map(data => data.json().token);

        loginObservable.subscribe(data => this.token = data);

        return loginObservable.map(value => !!value);
    }
}
