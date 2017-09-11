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
import {SurveyCardComponent} from './survey-tabs/shared/survey-card/survey-card.component';
import {ConsoleSharedModule} from './survey-tabs/shared/console-shared.module';

const ROUTES: Routes = [
  {
    path: 'console', component: ConsoleComponent, canActivate: [AuthGuardService],
    children: [
      {path: '', component: ConsoleMainComponent},
      {path: 'new', component: NewSurveyComponent}
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    HttpModule,
    SharedModule,
    ConsoleSharedModule,
    FormsModule,
    ReactiveFormsModule,
    MdButtonModule,
    MdTabsModule,
    MdInputModule,
    MdSelectModule,
    MdRadioModule,
    RouterModule.forChild(ROUTES)
  ],
  declarations: [
    ConsoleComponent,
    ProfileComponent,
    ConsoleTabsComponent,
    SurveysToolboxComponent,
    ConsoleMainComponent,
    SurveysTabComponent,
    NewSurveyComponent
  ],
  providers: [UserService, AuthGuardService, SurveyService, ExtendableHttp],
  exports: [RouterModule]
})
export class ConsoleModule {

}
