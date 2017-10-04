import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SurveyCardComponent} from './survey-card/survey-card.component';
import {MdButtonModule, MdCardModule} from '@angular/material';
import {RouterModule} from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    MdCardModule,
    RouterModule,
    MdButtonModule
  ],
  declarations: [
    SurveyCardComponent
  ],
  exports: [
    SurveyCardComponent
  ]
})
export class ConsoleSharedModule {

}
