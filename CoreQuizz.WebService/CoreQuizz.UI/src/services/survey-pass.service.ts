import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {ServiceResponse} from '../classes/service-response.class';

@Injectable()
export class SurveyPassService {
  submitSurvey(): Observable<ServiceResponse<boolean>> {
    return new Observable(subscriber => {
      setTimeout(() => {
        subscriber.next(null);
      }, 1000);
    });
  }
}
