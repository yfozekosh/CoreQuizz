import {NgModule} from '@angular/core';
import {MdButtonModule, MdInputModule} from '@angular/material';
import {SignInFormComponent} from './sign-in-form.component';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MdInputModule,
        MdButtonModule,
        FormsModule,
        RouterModule
    ],
    declarations: [SignInFormComponent],
    exports: [SignInFormComponent]
})
export class SignInFormModule {
}
