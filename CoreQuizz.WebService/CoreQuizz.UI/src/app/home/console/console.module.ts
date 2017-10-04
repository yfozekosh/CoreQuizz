import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuardService} from '../../../services/auth-guard.service';
import {ConsoleComponent} from './console.component';
import {UserService} from '../../../services/user.service';
import {SharedModule} from '../../shared/shared.module';
import {ProfileComponent} from './profile/profile.component';
import {MdButtonModule, MdInputModule, MdRadioModule, MdSelectModule, MdTabsModule} from '@angular/material';
import {ConsoleTabsComponent} from './survey-tabs/console-tabs.component';
import {SurveysTabComponent} from './survey-tabs/surveys-tab/surveys-tab.component';
import {SurveysToolboxComponent} from './survey-tabs/surveys-tab/surveys-toolbox/surveys-toolbox.component';
import {ConsoleMainComponent} from './console-main/console-main.component';
import {NewSurveyComponent} from './new-survey/new-survey.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {SurveyService} from '../../../services/survey.service';
import {ExtendableHttp} from '../../../services/extendable-http';
import {HttpModule} from '@angular/http';
import {ConsoleSharedModule} from './survey-tabs/shared/console-shared.module';
import {SearchPageComponent} from './search-page/search-page/search-page.component';
import {SurveyEditPageComponent} from './survey-edit-page/survey-edit-page.component';
import {SurveyPassPageComponent} from './survey-pass-page/survey-pass-page.component';
import {SurveyEditPageModule} from './survey-edit-page/survey-edit-page.module';
import {ModelDefinition} from '../../../model/index';

const ROUTES: Routes = [
  {
    path: 'console', component: ConsoleComponent,
    children: [
      {path: '', component: ConsoleMainComponent, canActivate: [AuthGuardService]},
      {path: 'new', component: NewSurveyComponent, canActivate: [AuthGuardService]},
      {path: 'search', component: SearchPageComponent},
      {
        path: 'survey/:id',
        component: SurveyPassPageComponent,
        canActivate: [AuthGuardService]
      },
      {path: 'survey/:id/edit', component: SurveyEditPageComponent, canActivate: [AuthGuardService]}
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,


    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule,

    SharedModule,
    ConsoleSharedModule,
    SurveyEditPageModule,

    RouterModule.forChild(ROUTES)
  ],
  declarations: [
    ConsoleComponent,
    ProfileComponent,
    ConsoleTabsComponent,
    SurveysToolboxComponent,
    ConsoleMainComponent,
    SurveysTabComponent,
    NewSurveyComponent,
    SurveyPassPageComponent,
    SearchPageComponent
  ],
  providers: [
    UserService,
    AuthGuardService,
    SurveyService,
    ExtendableHttp,
    {provide: 'QuestionDefinitionModel', useValue: ModelDefinition}
  ],
  exports: [RouterModule]
})
export class ConsoleModule {

}
