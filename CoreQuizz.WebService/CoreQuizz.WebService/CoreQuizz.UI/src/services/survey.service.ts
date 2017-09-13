import {ExtendableHttp} from './extendable-http';
import {ApiRoutes} from '../classes/api-routes.config';
import {ErrorServiceResponse, OkServiceResponse, ServiceResponse} from '../classes/service-response.class';
import {Observable} from 'rxjs/Observable';
import {Survey} from '../model/survey.class';
import {Injectable} from '@angular/core';
import {Headers} from '@angular/http';

@Injectable()
export class SurveyService {
  constructor(private _http: ExtendableHttp) {
    this.produceResponse = this.produceResponse.bind(this);
  }

  getSurveys(username?: string, accessCode?: number): Observable<ServiceResponse<Survey[]>> {
    const params = {};
    if (username) {
      params['username'] = username;
    }
    if (accessCode) {
      params['accessCode'] = accessCode;
    }

    return this._http.get(ApiRoutes.survey.getAll, {params})
      .map(d => d.json())
      .map(this.produceResponse(this.processSurveyDates));
  }

  getGlobalSurveys(searchText?: string, itemsOnPage?: number, pageNumber?: number): Observable<ServiceResponse<Survey[]>> {
    const params = {};
    if (searchText) {
      params['searchText'] = searchText;
    }
    if (itemsOnPage) {
      params['itemsOnPage'] = itemsOnPage;
    }
    if (pageNumber) {
      params['pageNumber'] = pageNumber;
    }

    return this._http.get(ApiRoutes.survey.getGlobal, {params})
      .map(d => d.json())
      .map(this.produceResponse(this.processSurveyDates));
  }

  createSurvey(title: string, access: number, description?: string): Observable<ServiceResponse<boolean>> {
    if (!title) {
      throw new Error('title should be specified');
    }
    const headers = new Headers();
    headers.set('Content-Type', 'application/json');

    const body = {
      title,
      description,
      access
    };

    return this._http.post(ApiRoutes.survey.create, body, {headers: headers})
      .map(d => d.json())
      .map(this.produceResponse(value => value));
  }

  getSurvey(id: number): Observable<ServiceResponse<Survey>> {
    return this._http.get(ApiRoutes.survey.get(id))
      .map(d => d.json())
      .map(this.produceResponse(value => this.processSurveyDates([value])[0]));
  }

  private processSurveyDates(args: Survey[]) {
    return args.map(s => ({
      ...s,
      createdDate: new Date(s.createdDate),
      modifiedDate: new Date(s.modifiedDate)
    }));
  }

  private produceResponse(callback: (value) => any) {
    return (d: any) => {
      if (d.isCritical) {
        throw new Error(d.error);
      }

      if (d.isSuccess) {
        let val = d;
        val = callback(d.value);

        return new OkServiceResponse(val);
      } else {
        return new ErrorServiceResponse(d.error);
      }
    };
  }
}
