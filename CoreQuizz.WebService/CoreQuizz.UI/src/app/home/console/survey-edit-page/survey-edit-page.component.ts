import {Component, OnInit} from '@angular/core';
import {SurveyWithDefinition} from '../../../../model/survey.class';
import {SurveyService} from '../../../../services/survey.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-survey-page',
  templateUrl: 'survey-edit-page.component.html',
  styleUrls: ['survey-edit-page.component.scss']
})
export class SurveyEditPageComponent implements OnInit {
  surveyId: number;
  survey: SurveyWithDefinition;

  constructor(private _surveyService: SurveyService, private _route: ActivatedRoute, private _router: Router) {
  }

  ngOnInit() {
    this._route.params.subscribe(params => {
      const id = params['id'];
      if (isNaN(id)) {
        this._router.navigateByUrl('/404');
      }

      this.surveyId = parseInt(id, 10);
    });

    this._surveyService.getSurveyForEdit(this.surveyId).toPromise().then(d => {
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

  handleAccessChange(access: number) {
    this.survey.access = access;
  }
}
