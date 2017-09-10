import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuardService} from '../../../services/auth-guard.service';
import {ConsoleComponent} from './console.component';
import {UserService} from '../../../services/user.service';
import {SharedModule} from '../../shared/shared.module';
import {ProfileComponent} from './profile/profile.component';
import {MdButtonModule, MdSelect, MdTabsModule} from '@angular/material';
import {ConsoleTabsComponent} from './survey-tabs/console-tabs.component';
import {SurveysTabComponent} from './survey-tabs/surveys-tab/surveys-tab.component';
import {SurveysToolboxComponent} from './survey-tabs/surveys-tab/surveys-toolbox/surveys-toolbox.component';

const ROUTES: Routes = [
  {path: 'console', component: ConsoleComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MdButtonModule,
    MdTabsModule,
    MdSelect,
    RouterModule.forChild(ROUTES)
  ],
  declarations: [
    ConsoleComponent,
    ProfileComponent,
    ConsoleTabsComponent,
    SurveysToolboxComponent,
    SurveysTabComponent
  ],
  providers: [UserService, AuthGuardService],
  exports: [RouterModule]
})
export class ConsoleModule {

}
