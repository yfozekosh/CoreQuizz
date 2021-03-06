import {Component, Input} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {InputQuestionDefinition} from '../../../../../../model/question-definition.class';

@Component({
  selector: 'app-input-definition-view',
  templateUrl: 'input-definition-view.component.html',
  styleUrls: ['input-definition-view.component.scss']
})
export class InputDefinitionViewComponent extends DefinitionComponent {
  @Input() question: InputQuestionDefinition;
  definition = InputQuestionDefinition;
}
