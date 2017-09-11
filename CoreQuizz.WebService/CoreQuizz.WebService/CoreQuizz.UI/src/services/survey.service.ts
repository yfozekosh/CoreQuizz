import {ExtendableHttp} from './extendable-http';
import {ApiRoutes} from '../classes/api-routes.config';
import {ErrorServiceResponse, OkServiceResponse, ServiceResponse} from '../classes/service-response.class';
import {Observable} from 'rxjs/Observable';
import {Survey} from '../classes/survey.class';
import {Injectable} from '@angular/core';
import {Headers} from '@angular/http';

@Injectable()
export class SurveyService {
  constructor(private _http: ExtendableHttp) {
  }

  getSurveys(username?: string, accessCode?: number): Observable<ServiceResponse<Survey[]>> {
    const params = {};
    if (username) {
      params['username'] = username;
    }
    if (accessCode) {
      params['accessCode'] = accessCode;
    }

    const httpRespose = this._http.get(ApiRoutes.survey.get, {
      params
    }).map(d => d.json())
      .map(d => {
        if (d.isCritical) {
          throw new Error(d.error);
        }

        if (d.isSuccess) {
          return new OkServiceResponse(this.processSurveyDate(...d.value));
        } else {
          return new ErrorServiceResponse(d.error);
        }
      });

    return httpRespose;
  }

  createSurvey(title: string, access: number, description?: string): Observable<ServiceResponse<boolean>> {
    if (!title) {
      throw new Error('title should be specified');
    }
    const headers = new Headers();
    headers.set('Content-Type', 'application/json');

    const httpResponse = this._http.post(ApiRoutes.survey.create, {
      title,
      description,
      access
    }, {
      headers: headers
    })
      .map(d => d.json())
      .map(d => {
        if (d.isCritical) {
          throw new Error(d.error);
        }

        if (d.isSuccess) {
          return new OkServiceResponse(true);
        } else {
          return new ErrorServiceResponse(d.error);
        }
      });

    return httpResponse;
  }

  private processSurveyDate(...args: Survey[]) {
    return args.map(s => ({
      ...s,
      createdDate: new Date(s.createdDate),
      modifiedDate: new Date(s.modifiedDate)
    }));
  }
}
