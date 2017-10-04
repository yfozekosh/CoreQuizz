import {Http, RequestOptionsArgs, Headers} from '@angular/http';
import {UserService} from './user.service';
import {Inject, Injectable} from '@angular/core';

export interface IHttpMiddleware {
  execute(options: RequestOptionsArgs);
}

export abstract class HttpMiddleware implements IHttpMiddleware {
  constructor(private next: IHttpMiddleware) {

  }

  abstract invoke(options: RequestOptionsArgs): RequestOptionsArgs;

  execute(options: RequestOptionsArgs) {
    if (!options) {
      options = {};
    }
    const result = this.invoke(options);
    if (this.next) {
      return this.next.execute(result);
    } else {
      return result;
    }
  }
}

export class AuthMiddleware extends HttpMiddleware {
  constructor(protected _userService: UserService, next: IHttpMiddleware) {
    super(next);
  }

  invoke(options: RequestOptionsArgs): RequestOptionsArgs {
    if (!this._userService.isLoggedIn()) {
      return options;
    }

    if (!options.headers) {
      options.headers = new Headers();
    }

    options.headers.append('Authorization', `Bearer ${this._userService.token.token}`);

    return options;
  }
}

@Injectable()
export class ExtendableHttp {
  middlewares: IHttpMiddleware;

  constructor(private _http: Http, private _userService: UserService) {
    this.middlewares = new AuthMiddleware(_userService, null);
  }

  get(url: string, options?: RequestOptionsArgs) {
    return this._http.get(url, this.getOptions(options));
  }

  post(url: string, body, options?: RequestOptionsArgs) {
    return this._http.post(url, body, this.getOptions(options));
  }

  put(url: string, options?: RequestOptionsArgs) {
    return this._http.put(url, this.getOptions(options));
  }

  patch(url: string, options?: RequestOptionsArgs) {
    return this._http.patch(url, this.getOptions(options));
  }

  delete(url: string, options?: RequestOptionsArgs) {
    return this._http.delete(url, this.getOptions(options));
  }

  getOptions(args: RequestOptionsArgs): RequestOptionsArgs {
    return this.middlewares.execute(args);
  }
}
