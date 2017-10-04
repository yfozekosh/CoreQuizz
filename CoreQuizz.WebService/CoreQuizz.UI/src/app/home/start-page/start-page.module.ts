import {LoginFormModule} from '../../components/login-form/login-form.module';
import {SignInFormModule} from '../../components/sign-in-form/sign-in-form.module';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home.component';
import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import {JumbotronComponent} from './jumbotron/jumbotron.component';
import {UserService} from '../../../services/user.service';

const ROUTES: Routes = [
  {path: '', component: HomeComponent}
];

@NgModule({
  imports: [
    CommonModule,
    LoginFormModule,
    SignInFormModule,
    SharedModule,
    RouterModule.forChild(ROUTES)
  ],
  declarations: [
    HomeComponent,
    JumbotronComponent
  ],
  providers: [UserService],
  exports: [
    RouterModule
  ]
})
export class StartPageModule {
}
