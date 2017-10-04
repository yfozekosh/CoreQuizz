import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CommonModule} from '@angular/common';
import {SurveyEditHeaderComponent} from './components/survey-edit-header/survey-edit-header.component';
import {
  MdProgressSpinnerModule,
  MdButtonModule,
  MdCheckboxModule,
  MdInputModule,
  MdRadioModule,
  MdSelectModule,
  MdTabsModule, MdDialog, MdDialogModule
} from '@angular/material';
import {AuthGuardService} from '../../../../services/auth-guard.service';
import {SurveyEditPageComponent} from './survey-edit-page.component';
import {SurveyEditTabComponent} from './tabs/survey-edit-tab/survey-edit-tab.component';
import {SurveyPrevireTabComponent} from './tabs/survey-preview-tab/survey-preview-tab.component';
import {DeleteDialogComponent, SurveySettingsTabComponent} from './tabs/survey-settings-tab/survey-settings-tab.component';
import {EditBlockContainerComponent} from './components/edit-block-container/edit-block-container.component';
import {SurveyQuestionDefinitionComponent} from './components/survey-question-definition/survey-question-definition.component';
import {DefinitionHostDirective} from './components/survey-question-definition/definition-host.directive';
import {AsideNavigationComponent} from './components/aside-navigation/aside-navigation.component';
import {ConsoleSharedModule} from '../shared/console-shared.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

const ROUTES: Routes = [
  {path: 'survey/:id/edit', component: SurveyEditPageComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule,
    MdCheckboxModule,
    MdProgressSpinnerModule,
    MdDialogModule,

    ConsoleSharedModule,

    RouterModule.forChild(ROUTES),
  ],
  declarations: [
    SurveyEditHeaderComponent,
    SurveyEditPageComponent,

    SurveyEditTabComponent,
    SurveyPrevireTabComponent,
    SurveySettingsTabComponent,

    EditBlockContainerComponent,
    SurveyQuestionDefinitionComponent,
    DeleteDialogComponent,

    AsideNavigationComponent,
  ],

  exports: [
    RouterModule
  ]
})
export class SurveyEditPageModule {
}
