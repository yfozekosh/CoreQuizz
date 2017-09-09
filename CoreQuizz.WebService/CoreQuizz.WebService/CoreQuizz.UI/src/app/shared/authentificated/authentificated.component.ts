import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-authentificated',
    template: `
        <ng-container *ngIf="!isAuthenticated">
            <ng-content></ng-content>
        </ng-container>`
})
export class AuthenticatedComponent implements OnInit {
    public isAuthenticated = false;

    ngOnInit(): void {
        // ToDo : call auth service
    }
}
