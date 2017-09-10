import {Component} from '@angular/core';

@Component({
  selector: 'app-surveys-toolbox',
  templateUrl: 'surveys-toolbox.component.html',
  styleUrls: ['surveys-toolbox.component.scss']
})
export class SurveysToolboxComponent {
  accessTypes = [
    {code: 0, display: 'All'},
    {code: 1, display: 'Public'},
    {code: 2, display: 'Private (Not implemented)'}
  ];

  selectedType = 0;
}
