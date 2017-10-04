import {Component, ElementRef, Input, OnInit, QueryList, ViewChildren} from '@angular/core';
import {DefinitionComponent} from '../definition-component';
import {RadioQuestionDefinition} from '../../../../../../../model/question-definition.class';
import {OptionsDefinition} from '../../../../../../../model/options-definition.class';

@Component({
  selector: 'app-radio-definition-view',
  templateUrl: 'radio-definition-view.component.html',
  styleUrls: ['radio-definition-view.component.scss']
})
export class RadioDefinitionViewComponent extends DefinitionComponent implements OnInit {
  @Input() question: RadioQuestionDefinition;
  @ViewChildren('optionRef') optionsRef: QueryList<ElementRef>;

  selectedValue: string;
  definition = RadioQuestionDefinition;

  static getDisplay() {
    return 'Radio Buttons';
  }

  ngOnInit(): void {
    if (!this.question.options) {
      this.question.options = [];
    }
    const checked = this.question.options.find(x => x.isSelected);
    if (checked) {
      this.selectedValue = checked.value;
    }
  }

  handleNew(e) {
    this.question.options.push(new OptionsDefinition('', false));
  }

  handleDelete(option: OptionsDefinition) {
    const index = this.question.options.indexOf(option);
    if (index !== -1) {
      this.question.options.splice(index, 1);
    }
  }

  get isSomeSelected(): boolean {
    return this.question.options.some(o => o.isSelected);
  }
}
