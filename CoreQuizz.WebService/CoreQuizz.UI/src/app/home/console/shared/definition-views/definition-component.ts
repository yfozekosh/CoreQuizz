import {EventEmitter, Output} from '@angular/core';
import {QuestionDefinition} from '../../../../../model/question-definition.abstract';

export abstract class DefinitionComponent {
  @Output() onTypeChange = new EventEmitter<any>();
  @Output() questionChange = new EventEmitter<QuestionDefinition>();
  abstract question: QuestionDefinition;
  abstract definition: any;
}
