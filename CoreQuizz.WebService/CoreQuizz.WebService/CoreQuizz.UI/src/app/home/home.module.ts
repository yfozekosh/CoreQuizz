import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './start-page/home.component';
import {SharedModule} from '../shared/shared.module';
import {JumbotronComponent} from './start-page/jumbotron/jumbotron.component';
import {SignInFormModule} from '../components/sign-in-form/sign-in-form.module';
import {NotFoundComponent} from './not-found/not-found.component';
import {UserService} from '../../services/user.service';
import {AuthGuardService} from '../../services/auth-guard.service';
import {ConsoleModule} from './console/console.module';
import {StartPageModule} from './start-page/start-page.module';

export const ROUTES: Routes = [
  {path: '404', component: NotFoundComponent},
  {path: '**', redirectTo: '/404'}
];


@NgModule({
  imports: [
    StartPageModule,
    ConsoleModule,
    RouterModule.forRoot(ROUTES)
  ],
  declarations: [NotFoundComponent],
  exports: [RouterModule]
})
export class HomeModule {
}
