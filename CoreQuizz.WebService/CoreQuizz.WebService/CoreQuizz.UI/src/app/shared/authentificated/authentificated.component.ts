import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-authentificated',
    template: `
        <div [hidden]="isAuthenticated">
            <ng-content></ng-content>
        </div>`
})
export class AuthenticatedComponent implements OnInit {
    public isAuthenticated = false;

    ngOnInit(): void {
        // ToDo : call auth service
    }
}
