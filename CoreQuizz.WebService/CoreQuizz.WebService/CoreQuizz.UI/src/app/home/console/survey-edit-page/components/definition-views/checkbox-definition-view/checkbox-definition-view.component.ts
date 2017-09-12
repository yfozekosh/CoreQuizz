import {Component, Input} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {CheckboxQuestionDefinition} from '../../../../../../../model/question-definition.class';

@Component({
  selector: 'app-checkbox-definition-view',
  templateUrl: 'checkbox-definition-view.component.html',
  styleUrls: ['checkbox-definition-view.component.scss']
})
export class CheckboxDefinitionViewComponent implements DefinitionComponent {
  @Input() question: CheckboxQuestionDefinition;
}
