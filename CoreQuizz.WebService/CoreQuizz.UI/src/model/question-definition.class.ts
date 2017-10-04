import {QuestionDefinition, QuestionWithOptionsDefinition} from './question-definition.abstract';
import {OptionsDefinition} from './options-definition.class';

export class InputQuestionDefinition extends QuestionDefinition {
  type = 'input';
  defaultValue: string;
}

export class RadioQuestionDefinition extends QuestionWithOptionsDefinition {
  type = 'radio';

  constructor(label?: string, options?: OptionsDefinition[]) {
    super(label, options);
  }
}

export class CheckboxQuestionDefinition extends QuestionWithOptionsDefinition {
  type = 'checkbox';

  constructor(label?: string, options?: OptionsDefinition[]) {
    super(label, options);
  }
}
