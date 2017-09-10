import {Component} from '@angular/core';

@Component({
  selector: 'app-surveys-toolbox',
  templateUrl: 'surveys-toolbox.component.html',
  styleUrls: ['surveys-toolbox.component.scss']
})
export class SurveysToolboxComponent {
  accessTypes = [
    'Public',
    'Private (Not implemented)',
    ''
  ]
}
