import {Component, OnInit} from '@angular/core';
import {SurveyService} from '../../../../services/survey.service';
import {ActivatedRoute, Router} from '@angular/router';
import {SurveyWithDefinition} from '../../../../model/survey.class';
import {SurveyPassService} from '../../../../services/survey-pass.service';

@Component({
  selector: 'app-survey-page',
  templateUrl: './survey-page.component.html',
  styleUrls: ['./survey-page.component.scss']
})
export class SurveyPageComponent implements OnInit {
  private surveyId: number;
  survey: SurveyWithDefinition;

  constructor(private _surveyService: SurveyService,
              private _route: ActivatedRoute,
              private _router: Router,
              private _surveyPassService: SurveyPassService) {
  }

  ngOnInit() {
    this._route.params.subscribe(params => {
      const id = params['id'];
      if (isNaN(id)) {
        this._router.navigateByUrl('/404');
      }

      this.surveyId = parseInt(id, 10);
    });

    this._surveyService.getSurveyWithDefinitions(this.surveyId).toPromise().then(d => {
      if (d.isSuccess) {
        this.survey = d.value;
        if (!this.survey.questionDefinitions) {
          this.survey.questionDefinitions = [];
        }
      } else {
        // TODO: call error service;
      }
    });
  }

  handleSubmit() {
    this._surveyPassService.submitSurvey().subscribe(d => {
      this._router.navigateByUrl('/console');
    });
  }
}
