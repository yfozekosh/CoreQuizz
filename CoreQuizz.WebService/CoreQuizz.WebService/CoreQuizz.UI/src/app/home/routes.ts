import {Route} from '@angular/router';
import {HomeComponent} from './start/home.component';
import {NotFoundComponent} from './not-found/not-found.component';
import {AuthGuardService} from '../../services/auth-guard.service';

export const routes: Route[] = [
  {path: '', component: HomeComponent},
  {path: '404', component: NotFoundComponent},
  {path: '**', redirectTo: '/404'}
];
