import {Component, OnInit} from '@angular/core';
import {QuestionView} from '../question-view.abstract-component';
import {RadioQuestionDefinition} from '../../../../../../model/question-definition.class';

@Component({
  selector: 'app-radio-question-view',
  templateUrl: './radio-question-view.component.html',
  styleUrls: ['./radio-question-view.component.scss']
})
export class RadioQuestionViewComponent extends QuestionView implements OnInit {
  question: RadioQuestionDefinition;

  constructor() {
    super();
  }

  ngOnInit() {
  }

}
