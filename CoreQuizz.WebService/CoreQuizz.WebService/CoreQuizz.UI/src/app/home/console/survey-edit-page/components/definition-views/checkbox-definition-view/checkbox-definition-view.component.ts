import {Component, Input} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {CheckboxQuestionDefinition} from '../../../../../../../model/question-definition.class';
import {OptionsDefinition} from '../../../../../../../model/options-definition.class';

@Component({
  selector: 'app-checkbox-definition-view',
  templateUrl: 'checkbox-definition-view.component.html',
  styleUrls: ['checkbox-definition-view.component.scss']
})
export class CheckboxDefinitionViewComponent extends DefinitionComponent {
  @Input() question: CheckboxQuestionDefinition;
  definition = CheckboxQuestionDefinition;

  handleNew(e) {
    this.question.options.push(new OptionsDefinition('', false));
  }

  handleDelete(option: OptionsDefinition) {
    const index = this.question.options.indexOf(option);
    if (index !== -1) {
      this.question.options.splice(index, 1);
    }
  }

  handleInput(option: OptionsDefinition) {
    this.questionChange.emit(this.question);
  }
}
