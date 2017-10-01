import {Component, OnDestroy, OnInit} from '@angular/core';
import {SurveyWithDefinition} from '../../../../../../model/survey.class';
import {InputQuestionDefinition} from '../../../../../../model/question-definition.class';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/interval';
import {Subscription} from 'rxjs/Subscription';
import {SurveyService} from '../../../../../../services/survey.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-survey-edit-tab',
  templateUrl: 'survey-edit-tab.component.html',
  styleUrls: ['survey-edit-tab.component.scss']
})
export class SurveyEditTabComponent implements OnInit, OnDestroy {
  survey: SurveyWithDefinition;
  saveObservable: Subscription;
  surveyId: number;
  activeIndex = -1;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute) {
    route.params.subscribe(p => this.surveyId = p.id);
  }

  ngOnInit() {
    this.surveyService.getSurveyForEdit(this.surveyId).toPromise().then(d => {
      if (d.isSuccess) {
        this.survey = d.value;
        let isSending = false;
        this.saveObservable = Observable.interval(5000).subscribe(() => {
          if (!isSending) {
            isSending = true;
            this.surveyService.saveSurvey(this.survey).do(null, () => isSending = false).subscribe(s => {
              console.log(s);
              isSending = false;
            });
          }
        });
      } else {
        // TODO: call error service;
      }
    });
  }

  ngOnDestroy() {
    if (this.saveObservable) {
      this.saveObservable.unsubscribe();
    }
  }

  handleClick(number: number) {
    this.activeIndex = number;
  }

  handleNew() {
    if (!this.survey.questionDefinitions){
      this.survey.questionDefinitions = [];
    }
    this.survey.questionDefinitions.push(new InputQuestionDefinition('new question'));
  }
}
