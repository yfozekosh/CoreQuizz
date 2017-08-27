import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {routes} from './routes';
import {HomeComponent} from './start/home.component';
import {SharedModule} from '../shared/shared.module';
import {JumbotronComponent} from './start/jumbotron/jumbotron.component';
import {SignInFormModule} from '../components/sign-in-form/sign-in-form.module';
import {NotFoundComponent} from './not-found/not-found.component';
import {UserService} from '../../services/user.service';
import {AuthGuardService} from '../../services/auth-guard.service';
import {AuthService} from '../../services/auth.service';

@NgModule({
    imports: [
        SharedModule,
        SignInFormModule,
        RouterModule.forRoot(routes)
    ],
    declarations: [HomeComponent, JumbotronComponent, NotFoundComponent],
    providers: [UserService, AuthService, AuthGuardService],
    exports: [RouterModule]
})
export class HomeModule {
}
