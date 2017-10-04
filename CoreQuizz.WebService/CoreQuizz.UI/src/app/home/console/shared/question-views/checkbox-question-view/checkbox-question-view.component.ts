import {Component, OnInit} from '@angular/core';
import {QuestionView} from '../question-view.abstract-component';

@Component({
  selector: 'app-checkbox-question-view',
  templateUrl: './checkbox-question-view.component.html',
  styleUrls: ['./checkbox-question-view.component.scss']
})
export class CheckboxQuestionViewComponent extends QuestionView implements OnInit {
  constructor() {
    super();
  }

  ngOnInit() {
  }

}
