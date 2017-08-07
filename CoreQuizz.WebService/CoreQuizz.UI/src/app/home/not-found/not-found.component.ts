import {Component} from '@angular/core';

@Component({
    template: `
        <div>
            <img src="../../../assets/404.png" alt="Error. 404 Not Found">
        </div>
    `,
    styles: [`
        div {
            width: 100%;
            height: 100%;
            background-color: #ced9c6;
            display: flex;
            justify-content: center;
            align-items: center;
        }
    `]
})
export class NotFoundComponent {
}
