import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuardService} from '../../../services/auth-guard.service';
import {ConsoleComponent} from './console.component';
import {UserService} from '../../../services/user.service';
import {SharedModule} from '../../shared/shared.module';
import {ProfileComponent} from './profile/profile.component';
import {MdButtonModule, MdTabsModule} from '@angular/material';
import {SurveyTabsComponent} from './survey-tabs/survey-tabs.component';

const ROUTES: Routes = [
  {path: 'console', component: ConsoleComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MdButtonModule,
    MdTabsModule,
    RouterModule.forChild(ROUTES)
  ],
  declarations: [
    ConsoleComponent,
    ProfileComponent,
    SurveyTabsComponent
  ],
  providers: [UserService, AuthGuardService],
  exports: [RouterModule]
})
export class ConsoleModule {

}
