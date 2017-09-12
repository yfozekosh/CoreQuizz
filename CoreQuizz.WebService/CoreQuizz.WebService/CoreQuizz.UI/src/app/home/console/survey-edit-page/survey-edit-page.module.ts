import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CommonModule} from '@angular/common';
import {SurveyEditHeaderComponent} from './components/survey-edit-header/survey-edit-header.component';
import {MdInputModule, MdRadioModule, MdSelectModule, MdTabsModule, MdButtonModule} from '@angular/material';
import {AuthGuardService} from '../../../../services/auth-guard.service';
import {SurveyEditPageComponent} from './survey-edit-page.component';
import {SurveyEditTabComponent} from './tabs/survey-edit-tab/survey-edit-tab.component';
import {SurveyPrevireTabComponent} from './tabs/survey-preview-tab/survey-preview-tab.component';
import {SurveySettingsTabComponent} from './tabs/survey-settings-tab/survey-settings-tab.component';
import {EditBlockContainerComponent} from './components/edit-block-container/edit-block-container.component';
import {SurveyQuestionDefinitionComponent} from './components/survey-question-definition/survey-question-definition.component';

const ROUTES: Routes = [
  {path: 'survey/:id/edit', component: SurveyEditPageComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES),
    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule
  ],
  declarations: [
    SurveyEditHeaderComponent,
    SurveyEditPageComponent,

    SurveyEditTabComponent,
    SurveyPrevireTabComponent,
    SurveySettingsTabComponent,

    EditBlockContainerComponent,
    SurveyQuestionDefinitionComponent
  ],
  exports: [
    RouterModule
  ]
})
export class SurveyEditPageModule {
}
