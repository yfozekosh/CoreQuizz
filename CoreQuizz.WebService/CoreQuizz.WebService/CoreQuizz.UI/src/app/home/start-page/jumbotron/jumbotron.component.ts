import {Component, Input} from '@angular/core';

@Component({
    selector: 'app-jumbotron',
    templateUrl: 'jumbotron.component.html',
    styleUrls: ['jumbotron.component.scss']
})
export class JumbotronComponent {
  @Input() isRegister = true;
}
