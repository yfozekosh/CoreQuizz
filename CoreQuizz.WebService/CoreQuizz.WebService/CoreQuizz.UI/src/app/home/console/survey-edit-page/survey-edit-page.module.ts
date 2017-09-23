import {NgModule, Provider} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CommonModule} from '@angular/common';
import {SurveyEditHeaderComponent} from './components/survey-edit-header/survey-edit-header.component';
import {
  MdButtonModule, MdCheckbox, MdCheckboxModule, MdInputModule, MdRadioModule, MdSelectModule,
  MdTabsModule
} from '@angular/material';
import {AuthGuardService} from '../../../../services/auth-guard.service';
import {SurveyEditPageComponent} from './survey-edit-page.component';
import {SurveyEditTabComponent} from './tabs/survey-edit-tab/survey-edit-tab.component';
import {SurveyPrevireTabComponent} from './tabs/survey-preview-tab/survey-preview-tab.component';
import {SurveySettingsTabComponent} from './tabs/survey-settings-tab/survey-settings-tab.component';
import {EditBlockContainerComponent} from './components/edit-block-container/edit-block-container.component';
import {SurveyQuestionDefinitionComponent} from './components/survey-question-definition/survey-question-definition.component';
import {DefinitionViewComponents, DefinitionViewsModule} from './components/definition-views/definition-views.module';
import {DefinitionHostDirective} from './components/survey-question-definition/definition-host.directive';
import {AsideNavigationComponent} from './components/aside-navigation/aside-navigation.component';
import { QuestionTypeSelectComponent } from './components/definition-views/question-type-select/question-type-select.component';

const ROUTES: Routes = [
  {path: 'survey/:id/edit', component: SurveyEditPageComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule,
    MdCheckboxModule,

    DefinitionViewsModule,

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

    DefinitionHostDirective,
    AsideNavigationComponent,
  ],
  providers: [
    {provide: 'QuestionDefinitions', useValue: DefinitionViewComponents}
  ],

  exports: [
    RouterModule
  ]
})
export class SurveyEditPageModule {
}
