import {Component} from '@angular/core';
import {SurveyWithDefinition} from '../../../../../../model/survey.class';
import {
  CheckboxQuestionDefinition,
  InputQuestionDefinition,
  RadioQuestionDefinition
} from '../../../../../../model/question-definition.class';
import {OptionsDefinition} from '../../../../../../model/options-definition.class';

@Component({
  selector: 'app-survey-edit-tab',
  templateUrl: 'survey-edit-tab.component.html',
  styleUrls: ['survey-edit-tab.component.scss']
})
export class SurveyEditTabComponent {
  survey: SurveyWithDefinition;
  activeIndex = -1;

  constructor() {
    this.survey = new SurveyWithDefinition();
    this.survey.surveyName = 'example';
    this.survey.description = 'descr';
    this.survey.surveyId = 1;

    this.survey.questionDefinition = [
      new InputQuestionDefinition('input1'),
      new RadioQuestionDefinition('radio', [
        new OptionsDefinition('option1', false),
        new OptionsDefinition('option2', true)
      ]),
      new CheckboxQuestionDefinition('checkox1', [
        new OptionsDefinition('coption1', true),
        new OptionsDefinition('coption2', true),
        new OptionsDefinition('coption3', false)
      ])
    ];
  }

  handleClick(number: number) {
    this.activeIndex = number;
  }

  handleNew() {
    this.survey.questionDefinition.push(new InputQuestionDefinition('new question'));
  }
}
