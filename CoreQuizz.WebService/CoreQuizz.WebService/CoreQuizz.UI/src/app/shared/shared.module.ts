import {NgModule} from '@angular/core';
import {NavbarComponent} from './navbar/navbar.component';
import {AuthenticatedComponent} from './authentificated/authentificated.component';
import {CommonModule} from '@angular/common';
import {FooterComponent} from './footer/footer.component';
import {RouterModule} from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    FooterComponent
  ],
  declarations: [
    NavbarComponent,
    AuthenticatedComponent,
    FooterComponent
  ]
})
export class SharedModule {
}
