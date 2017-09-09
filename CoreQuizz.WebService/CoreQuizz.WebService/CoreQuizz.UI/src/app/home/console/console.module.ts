import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuardService} from '../../../services/auth-guard.service';
import {ConsoleComponent} from './console.component';
import {UserService} from '../../../services/user.service';

const ROUTES: Routes = [
  {path: 'console', component: ConsoleComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(ROUTES)
  ],
  declarations: [ConsoleComponent],
  providers: [UserService, AuthGuardService],
  exports: [RouterModule]
})
export class ConsoleModule {

}
