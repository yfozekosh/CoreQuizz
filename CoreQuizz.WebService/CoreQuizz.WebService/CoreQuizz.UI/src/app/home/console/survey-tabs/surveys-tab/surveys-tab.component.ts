import {Component, OnInit} from '@angular/core';
import {SurveyService} from '../../../../../services/survey.service';
import {Observable} from 'rxjs/Observable';
import {Survey} from '../../../../../classes/survey.class';

@Component({
  selector: 'app-survey-tab',
  templateUrl: 'surveys-tab.component.html',
  styleUrls: ['surveys-tab.component.scss']
})
export class SurveysTabComponent implements OnInit {
  surveys: Observable<Survey[]>;

  constructor(private _surveyService: SurveyService) {
  }

  ngOnInit(): void {
    this.surveys = this._surveyService.getSurveys().map(d => d.value);
  }
}
