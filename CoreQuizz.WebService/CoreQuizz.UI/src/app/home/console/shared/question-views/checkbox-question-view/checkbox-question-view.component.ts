import {Component, OnInit} from '@angular/core';
import {QuestionView} from '../question-view.abstract-component';
import {CheckboxQuestionDefinition} from '../../../../../../model/question-definition.class';

@Component({
  selector: 'app-checkbox-question-view',
  templateUrl: './checkbox-question-view.component.html',
  styleUrls: ['./checkbox-question-view.component.scss']
})
export class CheckboxQuestionViewComponent extends QuestionView implements OnInit {
  question: CheckboxQuestionDefinition;

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
