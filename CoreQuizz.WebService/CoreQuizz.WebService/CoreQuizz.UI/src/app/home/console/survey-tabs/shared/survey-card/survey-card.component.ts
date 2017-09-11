import {Component, Input} from '@angular/core';
import {Survey} from '../../../../../../classes/survey.class';

@Component({
  selector: 'app-survey-card',
  templateUrl: 'survey-card.component.html',
  styleUrls: ['survey-card.component.scss']
})
export class SurveyCardComponent {
  @Input() survey: Survey;
}
