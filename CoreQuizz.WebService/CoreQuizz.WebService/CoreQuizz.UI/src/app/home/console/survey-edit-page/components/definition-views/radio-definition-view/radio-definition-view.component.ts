import {Component, Input, OnInit} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {RadioQuestionDefinition} from '../../../../../../../model/question-definition.class';
import {OptionsDefinition} from '../../../../../../../model/options-definition.class';

@Component({
  selector: 'app-radio-definition-view',
  templateUrl: 'radio-definition-view.component.html',
  styleUrls: ['radio-definition-view.component.scss']
})
export class RadioDefinitionViewComponent implements DefinitionComponent, OnInit {
  @Input() question: RadioQuestionDefinition;
  selectedValue: string;

  ngOnInit(): void {
    const checked = this.question.options.find(x => x.isSelected);
    if (checked) {
      this.selectedValue = checked.value;
    }
  }

  handleNew() {
    this.question.options.push(new OptionsDefinition('', false));
  }

  handleDelete(option: OptionsDefinition) {
    console.log('delete');
    const index = this.question.options.indexOf(option);
    if (index !== -1) {
      this.question.options.splice(index, 1);
    }
  }
}
