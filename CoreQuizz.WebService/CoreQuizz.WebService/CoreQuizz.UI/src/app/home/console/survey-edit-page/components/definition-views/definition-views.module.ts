import {InputDefinitionViewComponent} from './input-definition-view/input-definition-view.component';
import {Injectable, NgModule} from '@angular/core';
import {
  MdButtonModule, MdCheckboxModule, MdInputModule, MdRadioModule, MdSelectModule,
  MdTabsModule
} from '@angular/material';
import {CheckboxDefinitionViewComponent} from './checkbox-definition-view/checkbox-definition-view.component';
import {RadioDefinitionViewComponent} from './radio-definition-view/radio-definition-view.component';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';

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
    InputDefinitionViewComponent,
    CheckboxDefinitionViewComponent,
    RadioDefinitionViewComponent
  ],
  entryComponents: DefinitionViewComponents,
  exports: DefinitionViewComponents
})
export class DefinitionViewsModule {
}
