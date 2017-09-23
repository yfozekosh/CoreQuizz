import {Component, EventEmitter, Inject, Input, Output} from '@angular/core';

@Component({
  selector: 'app-question-type-select',
  templateUrl: './question-type-select.component.html',
  styleUrls: ['./question-type-select.component.scss']
})
export class QuestionTypeSelectComponent {
  @Input() value: any;
  @Output() valueChange = new EventEmitter();

  constructor(@Inject('QuestionDefinitions') public definitions: { name: string, getDisplay: () => string }[]) {
  }
}
