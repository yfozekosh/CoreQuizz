import {InputDefinitionViewComponent} from './input-definition-view/input-definition-view.component';
import {NgModule} from '@angular/core';
import {
  MdButtonModule,
  MdCheckboxModule,
  MdInputModule,
  MdRadioModule,
  MdSelectModule,
  MdTabsModule
} from '@angular/material';
import {CheckboxDefinitionViewComponent} from './checkbox-definition-view/checkbox-definition-view.component';
import {RadioDefinitionViewComponent} from './radio-definition-view/radio-definition-view.component';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {QuestionTypeSelectComponent} from './question-type-select/question-type-select.component';

export const DefinitionViewComponents = [
  InputDefinitionViewComponent,
  CheckboxDefinitionViewComponent,
  RadioDefinitionViewComponent
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,

    MdCheckboxModule,
    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule
  ],
  declarations: [
    ...DefinitionViewComponents,
    QuestionTypeSelectComponent
  ],
  entryComponents: DefinitionViewComponents,
  exports: DefinitionViewComponents
})
export class DefinitionViewsModule {
}
