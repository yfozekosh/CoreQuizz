import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SurveyPageComponent} from './survey-page.component';

@NgModule({
  imports: [
    CommonModule
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
