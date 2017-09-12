import {Component, Input, OnInit} from '@angular/core';
import * as QuestionDefinitions from '../../../../../../model/question-definition.class';
import {QuestionDefinition} from '../../../../../../model/question-definition.abstract';

@Component({
  selector: 'app-survey-question-definition',
  templateUrl: 'survey-question-definition.component.html',
  styleUrls: ['survey-question-definition.component.scss']
})
export class SurveyQuestionDefinitionComponent implements OnInit {
  @Input() questionDefinition: QuestionDefinition;

  ngOnInit() {
    console.log('!!!!!!!!!!!!!!!!!!!');
    for (const key of Object.keys(QuestionDefinitions)) {
      if (this.questionDefinition instanceof QuestionDefinitions[key]) {
        console.log(key);
      }
    }
    console.log('!!!!!!!!!!!!!!!!!!!');
  }
}
