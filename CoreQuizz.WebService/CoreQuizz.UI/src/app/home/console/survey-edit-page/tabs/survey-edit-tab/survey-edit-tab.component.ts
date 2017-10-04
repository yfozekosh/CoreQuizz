import {Component, Input, OnChanges, OnDestroy, OnInit} from '@angular/core';
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
export class SurveyEditTabComponent implements OnInit, OnDestroy, OnChanges {
  @Input() survey: SurveyWithDefinition;
  saveObservable: Subscription;
  surveyId: number;
  activeIndex = -1;

  constructor(private surveyService: SurveyService, private route: ActivatedRoute) {
    route.params.subscribe(p => this.surveyId = p.id);
  }

  ngOnInit() {

  }

  ngOnChanges() {
    if (this.saveObservable) {
      this.saveObservable.unsubscribe();
    }

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
  }

  ngOnDestroy() {
    if (this.saveObservable) {
      this.saveObservable.unsubscribe();
      this.surveyService.saveSurvey(this.survey).subscribe();
    }
  }

  handleClick(number: number) {
    this.activeIndex = number;
  }

  handleNew() {
    if (!this.survey.questionDefinitions) {
      this.survey.questionDefinitions = [];
    }
    this.survey.questionDefinitions.push(new InputQuestionDefinition('New question'));
  }

  handleClone(index: number) {
    const cloned = JSON.parse(JSON.stringify(this.survey.questionDefinitions[index]));

    this.survey.questionDefinitions.splice(index, 0, cloned);
  }

  handleUp(index: number) {
    if (index === 0) {
      return;
    }

    const prevQ = this.survey.questionDefinitions[index - 1];
    this.survey.questionDefinitions[index - 1] = this.survey.questionDefinitions[index];
    this.survey.questionDefinitions[index] = prevQ;
  }

  handleDown(index: number) {
    if (index === this.survey.questionDefinitions.length - 1) {
      return;
    }

    const nextQ = this.survey.questionDefinitions[index + 1];
    this.survey.questionDefinitions[index + 1] = this.survey.questionDefinitions[index];
    this.survey.questionDefinitions[index] = nextQ;
  }

  handleDelete(index: number) {
    this.survey.questionDefinitions.splice(index, 1);
  }
}
