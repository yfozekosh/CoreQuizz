import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SurveyPageComponent} from './survey-page.component';
import {ConsoleSharedModule} from '../shared/console-shared.module';
import {
  MdButtonModule, MdCardModule, MdInputModule, MdRadioModule, MdSelectModule,
  MdTabsModule
} from '@angular/material';
import {SurveyPassService} from '../../../../services/survey-pass.service';

@NgModule({
  imports: [
    CommonModule,

    MdCardModule,
    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule,

    ConsoleSharedModule
  ],
  declarations: [
    SurveyPageComponent
  ],
  exports: [
    SurveyPageComponent
  ],
  providers: [
    SurveyPassService
  ]
})
export class SurveyPageModule {
}
