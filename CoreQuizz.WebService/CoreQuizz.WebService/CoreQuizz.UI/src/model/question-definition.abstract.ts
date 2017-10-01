import {OptionsDefinition} from './options-definition.class';

export abstract class QuestionDefinition {
  questionLabel: string;
  abstract type: string;

  constructor(label?: string) {
    this.questionLabel = label;
  }
}


export abstract class QuestionWithOptionsDefinition extends QuestionDefinition {
  options: OptionsDefinition[];

  constructor(label?: string, options?: OptionsDefinition[]) {
    super(label);
    this.options = options ? options : [];
  }
}
