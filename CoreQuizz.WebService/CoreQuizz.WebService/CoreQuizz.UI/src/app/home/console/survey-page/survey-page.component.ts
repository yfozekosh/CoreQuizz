import {Component, OnInit} from '@angular/core';
import {Survey} from '../../../../classes/survey.class';
import {SurveyService} from '../../../../services/survey.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-survey-page',
  templateUrl: 'survey-page.component.html',
  styleUrls: ['survey-page.component.scss']
})
export class SurveyCreationPageComponent implements OnInit {
  surveyId: number;
  survey: Survey;

  constructor(private _surveyService: SurveyService, private _route: ActivatedRoute, private _router: Router) {
  }

  ngOnInit() {
    this._route.params.subscribe(params => {
      const id = params['id'];
      if (isNaN(id)) {
        this._router.navigateByUrl('/404');
      }

      this.surveyId = parseInt(id, 10);
      this._surveyService.getSurvey(this.surveyId).map(d => d.value).subscribe(survey => {
        this.survey = survey.value.survey;
      });
    });
  }
}
