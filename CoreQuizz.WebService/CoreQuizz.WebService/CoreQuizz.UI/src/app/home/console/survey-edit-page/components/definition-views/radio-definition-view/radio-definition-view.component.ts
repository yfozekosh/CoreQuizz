import {Component, Input} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {RadioQuestionDefinition} from '../../../../../../../model/question-definition.class';

@Component({
  selector: 'app-radio-definition-view',
  templateUrl: 'radio-definition-view.component.html',
  styleUrls: ['radio-definition-view.component.scss']
})
export class RadioDefinitionViewComponent implements DefinitionComponent{
  @Input() question: RadioQuestionDefinition;
  selectedValue: string;


}
