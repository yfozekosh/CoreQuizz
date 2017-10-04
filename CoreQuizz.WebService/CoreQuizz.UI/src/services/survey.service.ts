import {ExtendableHttp} from './extendable-http';
import {ApiRoutes} from '../classes/api-routes.config';
import {ErrorServiceResponse, OkServiceResponse, ServiceResponse} from '../classes/service-response.class';
import {Observable} from 'rxjs/Observable';
import {Survey, SurveyWithDefinition} from '../model/survey.class';
import {Injectable} from '@angular/core';
import {Headers} from '@angular/http';

@Injectable()
export class SurveyService {
  lastSaved: string;

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
      .map(this.produceResponse(value => {
        const newValue: any = {...this.processSurveyDates([value])[0]};
        newValue.questionDefinitions = newValue.questions;
        delete newValue.questions;
        return newValue;
      }));
  }

  getSurveyForEdit(id: number): Observable<ServiceResponse<SurveyWithDefinition>> {
    return this._http.get(ApiRoutes.survey.edit(id))
      .map(d => d.json())
      .map(this.produceResponse(value => {
        const newValue: any = {...this.processSurveyDates([value])[0]};
        newValue.questionDefinitions = newValue.questions;
        delete newValue.questions;
        return newValue;
      }));
  }


  saveSurvey(survey: Survey) {
    if (this.lastSaved !== JSON.stringify(survey)) {
      console.log('saving survey');

      const headers = new Headers();
      headers.set('Content-Type', 'application/json');

      const body = survey;
      return this._http.post(ApiRoutes.survey.save, body, {headers})
        .map(d => d.json())
        .map(this.produceResponse(value => value))
        .do(() => {
          this.lastSaved = JSON.stringify(survey);
        });
    }
    return new Observable(subscriber => subscriber.next('nothing to save'));
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
