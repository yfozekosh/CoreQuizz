import {Component, OnInit} from '@angular/core';
import {QuestionView} from '../question-view.abstract-component';
import {InputQuestionDefinition} from '../../../../../../model/question-definition.class';

@Component({
  selector: 'app-input-question-view',
  templateUrl: './input-question-view.component.html',
  styleUrls: ['./input-question-view.component.scss']
})
export class InputQuestionViewComponent extends QuestionView implements OnInit {
  question: InputQuestionDefinition;

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
