import {NgModule} from '@angular/core';
import {NavbarComponent} from './navbar/navbar.component';
import {AuthenticatedComponent} from './authentificated/authentificated.component';
import {CommonModule} from '@angular/common';
import {FooterComponent} from './footer/footer.component';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
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
