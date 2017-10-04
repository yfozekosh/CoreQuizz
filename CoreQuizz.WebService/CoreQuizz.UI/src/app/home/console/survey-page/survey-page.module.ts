import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SurveyPageComponent} from './survey-page.component';
import {ConsoleSharedModule} from '../shared/console-shared.module';
import {MdCardModule} from '@angular/material';

@NgModule({
  imports: [
    CommonModule,

    MdCardModule,

    ConsoleSharedModule
  ],
  declarations: [
    SurveyPageComponent
  ],
  exports: [
    SurveyPageComponent
  ]
})
export class SurveyPageModule {
}
