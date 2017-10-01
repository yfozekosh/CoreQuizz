import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-survey-edit-header',
  templateUrl: 'survey-edit-header.component.html',
  styleUrls: ['survey-edit-header.component.scss']
})
export class SurveyEditHeaderComponent {
  @Input() surveyName: string;
  @Input() description = '';
  @Output() surveyNameChange = new EventEmitter<string>();
  @Output() descriptionChange = new EventEmitter<string>();

  set _surveyName(value: string) {
    this.surveyName = value;
    this.surveyNameChange.emit(this.surveyName);
  }

  set _descriptionName(value: string) {
    this.description = value;
    this.descriptionChange.emit(this.description);
  }
}
