import {QuestionDefinition, QuestionWithOptionsDefinition} from './question-definition.abstract';
import {OptionsDefinition} from './options-definition.class';

export class InputQuestionDefinition extends QuestionDefinition {
  defaultValue: string;
}

export class RadioQuestionDefinition extends QuestionWithOptionsDefinition {
  constructor(label?: string, options?: OptionsDefinition[]) {
    super(label, options);
  }
}

export class CheckboxQuestionDefinition extends QuestionWithOptionsDefinition {
  constructor(label?: string, options?: OptionsDefinition[]) {
    super(label, options);
  }
}
