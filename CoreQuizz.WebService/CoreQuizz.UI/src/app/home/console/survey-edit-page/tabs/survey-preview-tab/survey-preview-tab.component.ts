import {Component, Input} from '@angular/core';
import {SurveyWithDefinition} from '../../../../../../model/survey.class';

@Component({
  selector: 'app-survey-preview-tab',
  templateUrl: 'survey-preview-tab.component.html',
  styleUrls: ['survey-preview-tab.component.scss']
})
export class SurveyPrevireTabComponent {
  @Input() survey: SurveyWithDefinition;
}
