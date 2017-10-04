import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveyService} from '../../../../../services/survey.service';
import {Survey} from '../../../../../model/survey.class';
import {Subject} from 'rxjs/Subject';
import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'app-survey-tab',
  templateUrl: 'surveys-tab.component.html',
  styleUrls: ['surveys-tab.component.scss']
})
export class SurveysTabComponent implements OnInit, OnDestroy {
  surveys: Subject<Survey[]> = new Subject();
  originalSurveys: Survey[];

  private _getSurveySubscription: Subscription;
  private _searchBoxText: string;

  get searchBoxText(): string {
    return this._searchBoxText;
  }

  set searchBoxText(value: string) {
    this._searchBoxText = value;
    if (this.originalSurveys) {
      this.surveys.next(this.originalSurveys.filter(value2 => value2.surveyName.indexOf(value) !== -1));
    }
  }

  constructor(private _surveyService: SurveyService) {
  }

  ngOnInit(): void {
    this._getSurveySubscription = this._surveyService
      .getSurveys()
      .map(d => d.value)
      .subscribe(d => {
        this.originalSurveys = d;
        this.surveys.next(d);
      });
  }

  ngOnDestroy(): void {
    if (this._getSurveySubscription) {
      this._getSurveySubscription.unsubscribe();
    }
  }
}
